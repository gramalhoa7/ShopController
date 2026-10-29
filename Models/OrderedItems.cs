namespace ShopControllerAPI.Models;

public class OrderedItems
{
    public OrderedItems(Guid itemId, int quantity, decimal unitPrice)
    {
        ItemId = itemId;
        Quantity = quantity;
        UnitPrice = unitPrice;

        Id = Guid.NewGuid();
    }

    public Guid Id { get; init; }

    // Liga a linha ao pedido
    public Guid OrderId { get; set; }
    public Orders Order { get; set; } = null!;

    // Liga a linha ao item do estoque
    public Guid ItemId { get; set; }
    public Items Item { get; set; } = null!;

    public int Quantity { get; set; }

    // Preço gravado no momento da venda (se o preço do item mudar depois, o pedido antigo não muda)
    public decimal UnitPrice { get; set; }
}