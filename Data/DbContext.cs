using ShopControllerAPI.Data;
using Microsoft.EntityFrameworkCore;
using ShopControllerAPI.Models;
using Microsoft.Extensions.Options;

namespace ShopControllerAPI.Data;

public class DBContext : DbContext
{
    public DbSet<Items> Item { get; set; }
    public DbSet<Orders> Order { get; set; }
    public DbSet<Employees> Employee { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Shop;Trusted_Connection=True;TrustServerCertificate=True;");
        base.OnConfiguring(optionsBuilder);
    }
}
