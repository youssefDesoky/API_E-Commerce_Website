using System;
using E_Commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Persistence.Context;

public class IdentityStoreDbContext(DbContextOptions<IdentityStoreDbContext> options) : IdentityDbContext<AppUser>(options)
{
    override protected void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>().ToTable("Users");
        builder.Entity<IdentityRole>().ToTable("Roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        builder.Entity<Address>().ToTable("Addresses");

        builder.Ignore<IdentityUserLogin<string>>();
        builder.Ignore<IdentityUserToken<string>>();
        builder.Ignore<IdentityUserClaim<string>>();
        builder.Ignore<IdentityRoleClaim<string>>();
    }
}
