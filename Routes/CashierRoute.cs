using Microsoft.EntityFrameworkCore;
using ShopControllerAPI.Data;
using ShopControllerAPI.Models;

namespace ShopControllerAPI.Routes;

public static class CashierRoute
{
    public static void CashierRoutes(this WebApplication app)
    {
        //Novos caixas e gerencia deles
        app.MapPost("Cashiers", async (CashiersRequest req, DBContext context) =>
        {
            var cashier = new Cashiers(req.NCashier, req.Actived);
            await context.AddAsync(cashier);
            await context.SaveChangesAsync();
        });

        app.MapGet("Cashiers", async (DBContext context) =>
        {
            var cashier = await context.Cashier.ToListAsync();
            return Results.Ok(cashier);
        });

        app.MapDelete("Cashiers/{Id:guid}", async (Guid id, DBContext context) =>
        {
            var cashier = await context.Cashier.FirstOrDefaultAsync(o => o.Id == id);

            if (cashier == null) return Results.NotFound();

            cashier.Actived = false;

            await context.SaveChangesAsync();

            return Results.Ok(cashier);
        });

        //Abrimento e fechamento de sação de caixa
        app.MapPost("Cashiers/Open", async (Guid cashierId, Guid employeeId, decimal openingAmount, DBContext context) =>
        {
            var cashierSession = new CashiersSessions(cashierId, employeeId, openingAmount);

            await context.CashiersSession.AddAsync(cashierSession);
            await context.SaveChangesAsync();

            return Results.Ok(cashierSession);
        }); 

        app.MapPost ("Cashiers/{Id:guid}/close", async (Guid id, DBContext context) =>
        {
            var cashierSession = await context.CashiersSession.FirstOrDefaultAsync(o => o.Id == id);

            if (cashierSession == null) return Results.NotFound();
            
            cashierSession.Closed = DateTime.UtcNow;
            var cashSales = await context.OrderedItems
                .Where(l => l.Order.CashierId == cashierSession.Id
                    && l.Order.Exist
                    && l.Order.PaymantForm == "Dinheiro")
                    .SumAsync(l => l.Quantity * l.UnitPrice);

            cashierSession.ClosingAmount = cashierSession.OpeningAmount + cashSales;

            await context.SaveChangesAsync();

            return Results.Ok(new
            {
                cashierSession.Id,
                cashierSession.OpeningAmount,
                CashSales = cashSales,
                cashierSession.ClosingAmount
            });
        });
    }
}