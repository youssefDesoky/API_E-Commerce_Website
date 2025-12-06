using System.Reflection;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Persistence.Context;

public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<ProductBrand> ProductBrands { get; set; } = default!;
    public DbSet<ProductType> ProductTypes { get; set; } = default!;

    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; } = default!;
    public DbSet<OrderItem> OrderItems { get; set; } = default!;
}
