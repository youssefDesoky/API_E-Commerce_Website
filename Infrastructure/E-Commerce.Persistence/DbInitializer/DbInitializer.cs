using System;
using System.Text.Json;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Persistence.DbInitializer;

// public class DbInitializer
// {
//     private readonly StoreDbContext _context;

//     public DbInitializer(StoreDbContext context)
//     {
//         _context = context;
//     }
// }

public class DbInitializer(
    StoreDbContext context,
    IdentityStoreDbContext identityContext,
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager) : IDbInitializer
{
    public async Task InitializeAsync()
    {
        if (context.Database.GetPendingMigrations().Any())
            await context.Database.MigrateAsync(); // Apply any pending migrations

        // Product Brands Seeding
        if (!context.ProductBrands.Any())
        {
            var brandsData = await File.ReadAllTextAsync(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../Infrastructure/E-Commerce.Persistence/Context/DataSeed/brands.json")));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData, options);

            if (brands is not null && brands.Any())
            {
                await context.ProductBrands.AddRangeAsync(brands);
            }
        }

        // Product Types Seeding
        if (!context.ProductTypes.Any())
        {
            var typesData = await File.ReadAllTextAsync(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../Infrastructure/E-Commerce.Persistence/Context/DataSeed/types.json")));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData, options);

            if (types is not null && types.Any())
            {
                await context.ProductTypes.AddRangeAsync(types);
            }
        }

        // Products Seeding
        if (!context.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../Infrastructure/E-Commerce.Persistence/Context/DataSeed/products.json")));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var products = JsonSerializer.Deserialize<List<Product>>(productsData, options);

            if (products is not null && products.Any())
            {
                await context.Products.AddRangeAsync(products);
            }
        }

        // Delivery Methods Seeding
        if (!context.DeliveryMethods.Any())
        {
            var deliveryMethodsData = await File.ReadAllTextAsync(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../Infrastructure/E-Commerce.Persistence/Context/DataSeed/delivery.json")));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData, options);

            if (deliveryMethods is not null && deliveryMethods.Any())
            {
                await context.DeliveryMethods.AddRangeAsync(deliveryMethods);
            }
        }

        await context.SaveChangesAsync();
    }

    public async Task InitializeIdentityAsync()
    {
        // Create || Update Identity Database Schema
        if (identityContext.Database.GetPendingMigrations().Any())
            await identityContext.Database.MigrateAsync(); // Apply any pending migrations

        // Seed Identity Data
        if (!identityContext.Roles.Any())
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "SuperAdmin" },
                new IdentityRole { Name = "Admin" }
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }

        if (!identityContext.Users.Any())
        {
            var superAdmin = new AppUser
            {
                UserName = "superAdmin",
                DisplayName = "Super Admin",
                Email = "superadmin@example.com",
                PhoneNumber = "1234567890",

            };

            var adminUser = new AppUser
            {
                UserName = "admin",
                DisplayName = "Admin User",
                Email = "admin@example.com",
                PhoneNumber = "0987654321",

            };

            await userManager.CreateAsync(superAdmin, "P@ssw0rd");
            await userManager.CreateAsync(adminUser, "P@ssw0rd");

            await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
