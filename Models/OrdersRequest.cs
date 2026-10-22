namespace ShopControllerAPI.Models;

public class OrdersRequest
{
    public int Number { get; set; }
    public int Cashier { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public List<OrderedItems> ItemOrd { get; set; } = new();
    public string PaymantForm { get; set; }
    public string NTF { get; set; }
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
    int Cashier,
    decimal Total,
    string Status,
    DateTime Date,
    string Time,
    List<OrderedItemsResponse> ItemOrd,
    string PaymantForm,
    string NTF,
    bool Exist);
