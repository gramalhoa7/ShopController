namespace ShopControllerAPI.Models;

public class Items
{
    public Items(string name, string type, decimal value, decimal custValue, string barCode, int quant, string operation, bool exist)
    {
        Name = name;
        Type = type;
        Value = value;
        CustValue = custValue;
        BarCode = barCode;
        Quant = quant;
        Operation = operation;
        Date = DateTime.UtcNow;
        Exist = exist;

        Id = Guid.NewGuid();
    }
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Type { get; set; }
    public decimal Value { get; set; }
    public decimal CustValue { get; set; }
    public string BarCode { get; set; }
    public int Quant { get; set; }
    public string Operation { get; set; }
    public DateTime Date { get; set; }
    public bool Exist { get; set; }
}