using Microsoft.EntityFrameworkCore;
using ShopControllerAPI.Data;

namespace ShopControllerAPI.Models;

public static class FinanceRoute
{
    public static void FinanceRoutes(this WebApplication app)
    {
        app.MapPost("Receivables", async (ReceivablesRequest req, DBContext context) =>
        {
            var receivable = new Receivables(req.OrderId, req.Description, req.Amount, req.DueDate, req.ReceivedAt);

            await context.AddAsync(receivable);
            await context.SaveChangesAsync();
        }).RequireAuthorization(o => o.RequireRole("Adm", "Manager"));

        app.MapGet("Receivables", async (DBContext context) =>
        {
            var receivable = await context.Receivable.ToListAsync();
            return Results.Ok(receivable);
        }).RequireAuthorization(o => o.RequireRole("Adm", "Manager"));

        app.MapPut("Receivables/{Id:guid}", async (Guid id, ReceivablesRequest req, DBContext context) =>
        {
            var receivable = await context.Receivable.FirstOrDefaultAsync(x => x.Id == id);

            if (receivable == null) return Results.NotFound();

            receivable.OrderId = req.OrderId;
            receivable.Description = req.Description;
            receivable.Amount = req.Amount;
            receivable.DueDate = req.DueDate;
            receivable.ReceivedAt = req.ReceivedAt;

            await context.SaveChangesAsync();
            
            return Results.Ok(receivable); 
        }).RequireAuthorization(o => o.RequireRole("Adm", "Manager"));

        app.MapDelete("Receivables/{Id:guid}", async (Guid id, DBContext context) =>
        {
            var receivable = await context.Receivable.FirstOrDefaultAsync(x => x.Id == id);

            if (receivable == null) return Results.NotFound();

            context.Remove(receivable);
            await context.SaveChangesAsync();

            return Results.Ok(receivable);
        }).RequireAuthorization(o => o.RequireRole("Adm", "Manager"));
    }
}