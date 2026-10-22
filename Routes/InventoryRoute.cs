using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ShopControllerAPI.Data;
using ShopControllerAPI.Models;

namespace ShopControllerAPI.Routes;

public static class InvetoryRoute
{
    public static void InventoryRoutes(this WebApplication app)
    {
        app.MapPost("Inventory", async (ItemsRequest req, DBContext context) =>
        {
            var Item = new Items(req.Name, req.Type, req.Value, req.CustValue, req.BarCode, req.Quant, req.Operation, req.Date, req.Exist);
            await context.AddAsync(Item);
            await context.SaveChangesAsync();
        });

        app.MapGet("Inventory", async (DBContext context) =>
        {
            var item = await context.Item.ToListAsync();
            return Results.Ok(item);
        });

        app.MapPut("Inventory/{Id:guid}", async (Guid id, ItemsRequest req, DBContext context) =>
        {
            var item = await context.Item.FirstOrDefaultAsync(X => X.Id == id);

            if (item == null) return Results.NotFound();

            item.Name = req.Name;
            item.Type = req.Type;
            item.Value = req.Value;
            item.CustValue = req.CustValue;
            item.BarCode = req.BarCode;
            item.Quant = req.Quant;

            await context.SaveChangesAsync();
            
            return Results.Ok(item); 
        });

        app.MapDelete("Inventory/{Id:guid}", async (Guid id, DBContext context) =>
        {
            var item = await context.Item.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null) return Results.NotFound();

            item.Exist = false;

            await context.SaveChangesAsync();

            return Results.Ok(item);
        });
    }
}
