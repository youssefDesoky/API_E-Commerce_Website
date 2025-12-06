using System;
using E_Commerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Persistence.Context.Configurations;

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(dm => dm.Cost).HasColumnType("decimal(8,2)");
        
        builder.Property(dm => dm.ShortName).HasColumnType("nvarchar").HasMaxLength(50);
        
        builder.Property(dm => dm.Description).HasColumnType("nvarchar").HasMaxLength(100);

        builder.Property(dm => dm.DeliveryTime).HasColumnType("nvarchar").HasMaxLength(50);
    }
}
