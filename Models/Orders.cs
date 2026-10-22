using System.Globalization;

namespace ShopControllerAPI.Models;

public class Orders
{
    public Orders (int number, int cashier, decimal total, string status, DateTime date, string time, string paymantForm, string ntf, bool exist)
    {
        Number = number;
        Cashier = cashier;
        Total = total;
        Status = status;
        Date = date;
        Time = time;
        PaymantForm = paymantForm;
        NTF = ntf;
        Exist = exist;

        Id = Guid.NewGuid();
    }
    public Guid Id { get; init; }
    public int Number { get; set; }
    public int Cashier { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public List<OrderedItems> ItemOrd { get; set; } = new ();
    public string PaymantForm { get; set; }
    public string NTF { get; set; }
    public bool Exist { get; set; }
}