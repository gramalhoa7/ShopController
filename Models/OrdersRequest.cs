namespace ShopControllerAPI.Models;

public class OrdersRequest
{
    public int Number { get; set; }
    public Guid CashierId { get; set; }
    public CashiersSessions CashierSession { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public List<OrderedItems> ItemOrd { get; set; } = new();
    public decimal PayValue { get; set; }
    public string PaymantForm { get; set; }
    public string Ntf { get; set; }
    public bool Exist { get; set; }
}

public class OrderedItemsRequest
{
    public Guid ItemId { get; set; }
    public int Quant { get; set; }
}

// Respostas (evitam o erro de ciclo Order -> Lines -> Order no JSON)
public record OrderedItemsResponse(Guid ItemId, string ItemName, int Quantity, decimal UnitPrice);
 
public record OrdersResponse(
    Guid Id,
    int Number,
    Guid CashierId,
    string Status,
    DateTime Date,
    string Time,
    List<OrderedItemsResponse> ItemOrd,
    decimal PayValue,
    string PaymantForm,
    string Ntf,
    bool Exist);
