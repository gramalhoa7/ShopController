using Microsoft.EntityFrameworkCore;
using ShopControllerAPI.Data;
using ShopControllerAPI.Models;

namespace ShopControllerAPI.Routes;

public static class OrderRoute
{
    private static OrdersResponse ToResponse(Orders o) => new(
        o.Id, o.Number, o.CashierSessionId, o.Status, o.Date, o.Item.Select(l => new OrderedItemsResponse(l.ItemId, l.Item.Name, l.Quantity, l.UnitPrice)).ToList(),
        o.PayValue, o.PaymantForm, o.Ntf, o.Exist);
 
    public static void OrderRoutes(this WebApplication app)
    {
        //após passar no caixa
        app.MapPost("Order", async (OrdersRequest req, DBContext context) =>
        {
            if (req.Item.Count == 0)
                return Results.BadRequest("O pedido precisa ter pelo menos um item.");
 
            var order = new Orders(req.Number, req.CashierSessionId, req.Status, req.PayValue, req.PaymantForm, req.Ntf, req.Exist);
 
            foreach (var itemOrd in req.Item)
            {
                if (itemOrd.Quantity <= 0)
                    return Results.BadRequest("A quantidade deve ser maior que zero.");
 
                var item = await context.Item.FirstOrDefaultAsync(i => i.Id == itemOrd.ItemId);
 
                if (item == null)
                    return Results.BadRequest($"O item {itemOrd.ItemId} não existe.");
 
                if (item.Quant < itemOrd.Quantity)
                    return Results.BadRequest($"Estoque insuficiente para {item.Name} (disponível: {item.Quant}).");
 
                item.Quant -= itemOrd.Quantity;   // baixa o estoque
 
                // Value = preço de venda (CustValue parece ser o custo). Ajuste se for o contrário.
                order.Item.Add(new OrderedItems(item.Id, itemOrd.Quantity, item.Value));
            }
    
            decimal Total;
            Total = order.Item.Sum(l => l.Quantity * l.UnitPrice);
 
            await context.AddAsync(order);
            await context.SaveChangesAsync();   // salva pedido + linhas + estoque juntos
 
            return Results.Created($"/Order/{order.Id}", order.Id);
        });
 
        //mostrar todos os pedidos
        app.MapGet("Order", async (DBContext context) =>
        {
            var orders = await context.Order
                .Include(o => o.Item)
                .ThenInclude(l => l.Item)       // sem o Include, Lines voltaria vazia
                .ToListAsync();
 
            return Results.Ok(orders.Select(ToResponse));
        });
 
        //detalhes do pedido
        app.MapGet("Order/{Id:guid}", async (Guid id, DBContext context) =>
        {
            var order = await context.Order
                .Include(o => o.Item)
                .ThenInclude(l => l.Item)
                .FirstOrDefaultAsync(o => o.Id == id);
 
            if (order == null) return Results.NotFound();
 
            return Results.Ok(ToResponse(order));
        });
 
        // O PUT altera só o cabeçalho do pedido. Mexer nos itens é outra operação.
        app.MapPut("Order/{Id:guid}", async (Guid id, OrdersRequest req, DBContext context) =>
        {
            var order = await context.Order.FirstOrDefaultAsync(x => x.Id == id);
 
            if (order == null) return Results.NotFound();
 
            order.Number = req.Number;
            order.CashierSessionId = req.CashierSessionId;
            order.Status = req.Status;
            order.PayValue = req.PayValue;
            order.PaymantForm = req.PaymantForm;
            order.Ntf = req.Ntf;
            order.Exist = req.Exist;
 
            await context.SaveChangesAsync();
 
            return Results.Ok(order.Id);
        });
 
        // Faltava o {Id:guid} na rota e o SaveChangesAsync
        app.MapDelete("Order/{Id:guid}", async (Guid id, DBContext context) =>
        {
            var order = await context.Order.FirstOrDefaultAsync(x => x.Id == id);
 
            if (order == null) return Results.NotFound();
 
            order.Exist = false;
 
            await context.SaveChangesAsync();
 
            return Results.Ok(order.Id);
        });
    }
}