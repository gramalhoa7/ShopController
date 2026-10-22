namespace ShopControllerAPI.Models;

public class Employees
{
    public Employees(string name, int age, string department, bool employed)
    {
        Name = name;
        Age = age;
        Department = department;
        Employed = employed;

        Id = Guid.NewGuid();
    }
    public Guid Id { get; init; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Department { get; set; }
    public bool Employed { get; set; }
}