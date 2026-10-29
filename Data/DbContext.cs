using Microsoft.EntityFrameworkCore;
using ShopControllerAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ShopControllerAPI.Data;

public class DBContext : IdentityDbContext<EmployeeAuth>
{
    public DbSet<Items> Item { get; set; }
    public DbSet<Orders> Order { get; set; }
    public DbSet<OrderedItems> OrderedItems { get; set; }
    public DbSet<Cashiers> Cashier { get; set; }
    public DbSet<CashiersSessions> CashiersSession { get; set; }
    public DbSet<Employees> Employee { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Shop;Trusted_Connection=True;TrustServerCertificate=True;");
        base.OnConfiguring(optionsBuilder);
    }
}
