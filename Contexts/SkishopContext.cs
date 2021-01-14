using Microsoft.EntityFrameworkCore;

public class SkishopContext : DbContext
{
  public SkishopContext(DbContextOptions<SkishopContext> options)
    : base(options) {}

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=DB/skishop.db");
  }

  public DbSet<Category> Categories { get; set; }
  public DbSet<Customer> Customers { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<ProductCategory> ProductCategories { get; set; }
  public DbSet<ProductOrder> ProductOrders { get; set; }
}