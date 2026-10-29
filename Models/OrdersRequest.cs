public class OrdersRequest
{
    public int Number { get; set; }
    public Guid CashierSessionId { get; set; }
    public string Status { get; set; }
    public decimal PayValue { get; set; }
    public string PaymantForm { get; set; }
    public string Ntf { get; set; }
    public bool Exist { get; set; }

    // Cada item do pedido: só "qual produto" e "quantos"
    public List<OrderedItemsRequest> Item { get; set; } = new();
}

// Usado no PUT: altera só o cabeçalho, sem lista de itens
public class OrdersUpdateRequest
{
    public int Number { get; set; }
    public Guid CashierSessionId { get; set; }
    public string Status { get; set; }
    public decimal PayValue { get; set; }
    public string PaymantForm { get; set; }
    public string Ntf { get; set; }
    public bool Exist { get; set; }
}

public class OrderedItemsRequest
{
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
}

// Respostas (evitam o erro de ciclo Order -> Lines -> Order no JSON)
public record OrderedItemsResponse(Guid ItemId, string ItemName, int Quantity, decimal UnitPrice);

public record OrdersResponse(
    Guid Id,
    int Number,
    Guid CashierSessionId,
    string Status,
    DateTime Date,
    List<OrderedItemsResponse> ItemOrd,
    decimal PayValue,
    string PaymantForm,
    string Ntf,
    bool Exist);
