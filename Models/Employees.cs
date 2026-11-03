using Microsoft.AspNetCore.Identity;

namespace ShopControllerAPI.Models;

public class Employees : IdentityUser<Guid>
{
    public Employees(){} //para o identity

    public Employees(string name, int age, string department, bool employed)
    {
        Name = name;
        Age = age;
        Department = department;
        Employed = employed;
    }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Department { get; set; } = string.Empty;
    public bool Employed { get; set; } = true;
}