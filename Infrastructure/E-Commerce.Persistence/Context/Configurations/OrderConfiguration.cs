using System;
using E_Commerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Persistence.Context.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(o => o.ShippingAddress);

        builder.HasOne(o => o.DeliveryMethod)
               .WithMany()
               .HasForeignKey(o => o.DeliveryMethodId);

        builder.HasMany(o => o.OrderItems)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade); // When an Order is deleted, its OrderItems are also deleted

        builder.Property(o => o.Subtotal)
               .HasColumnType("decimal(8,2)");
    }
}
