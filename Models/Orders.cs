namespace ShopControllerAPI.Models;

public class Orders
{
    public Orders (int number, Guid cashierSessionId, string status, decimal payValue, string paymantForm, string ntf, bool exist)
    {
        Number = number;
        CashierSessionId = cashierSessionId;
        Status = status;
        Date = DateTime.UtcNow;
        PayValue = payValue;
        PaymantForm = paymantForm;
        Ntf = ntf;
        Exist = exist;

        Id = Guid.NewGuid();
    }
    public Guid Id { get; init; }
    public int Number { get; set; }
    public Guid CashierSessionId { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public List<OrderedItems> Item { get; set; } = new ();
    public decimal PayValue { get; set; }
    public string PaymantForm { get; set; }
    public string Ntf { get; set; }

    //operation
    
    public bool Exist { get; set; }
}