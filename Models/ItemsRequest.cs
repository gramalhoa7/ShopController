namespace ShopControllerAPI.Models;

public class ItemsRequest
{
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