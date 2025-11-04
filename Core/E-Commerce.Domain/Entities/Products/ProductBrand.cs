using System;

namespace E_Commerce.Domain.Entities.Products;

public class ProductBrand : Entity<int>
{
    public string Name { get; set; } = default!; // 'default!' here is same as using 'null!'
}
