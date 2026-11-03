using Microsoft.EntityFrameworkCore;
using ShopControllerAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ShopControllerAPI.Data;

public class DBContext : IdentityDbContext<Employees, IdentityRole<Guid>, Guid>
{
    public DbSet<Items> Item { get; set; }
    public DbSet<Orders> Order { get; set; }
    public DbSet<OrderedItems> OrderedItems { get; set; }
    public DbSet<Cashiers> Cashier { get; set; }
    public DbSet<CashiersSessions> CashiersSession { get; set; }
    //public DbSet<Employees> Employee { get; set; } usar context.Users
    public DbSet<Receivables> Receivable { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Shop;Trusted_Connection=True;TrustServerCertificate=True;");
        base.OnConfiguring(optionsBuilder);
    }
}
