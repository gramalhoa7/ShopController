using System.Globalization;

namespace ShopControllerAPI.Models;

public class Orders
{
    public Orders (int number, Guid cashierId, string status, string time, decimal payValue, string paymantForm, string ntf, bool exist)
    {
        Number = number;
        CashierId = cashierId;
        Status = status;
        Date = DateTime.UtcNow;
        Time = time;
        PayValue = payValue;
        PaymantForm = paymantForm;
        Ntf = ntf;
        Exist = exist;

        Id = Guid.NewGuid();
    }
    public Guid Id { get; init; }
    public int Number { get; set; }
    public Guid CashierId { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public List<OrderedItems> ItemOrd { get; set; } = new ();
    public decimal PayValue { get; set; }
    public string PaymantForm { get; set; }
    public string Ntf { get; set; }
    public bool Exist { get; set; }
}