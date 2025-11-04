using System;
using E_Commerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Persistence.Context.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
            .HasColumnType("nvarchar")
            .HasMaxLength(265);

        builder.Property(p => p.Description)
            .HasColumnType("nvarchar")
            .HasMaxLength(1024);

        builder.Property(p => p.PictureUrl)
            .HasColumnType("nvarchar")
            .HasMaxLength(256);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        // This Part Must Be Added Only If Foreign Key In Product Table is Named To different Name Than Navigation Property + "Id"
        builder.HasOne(p => p.ProductBrand)
            .WithMany()
            .HasForeignKey(p => p.BrandId);

        builder.HasOne(p => p.ProductType)
            .WithMany()
            .HasForeignKey(p => p.TypeId);
    }
}
