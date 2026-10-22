namespace ShopControllerAPI.Models;

public class EmployeesRequest
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Department { get; set; }
    public bool Employed { get; set; }
}