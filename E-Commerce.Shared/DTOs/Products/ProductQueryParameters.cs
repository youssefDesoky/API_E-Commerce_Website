using System;

namespace E_Commerce.Shared.DTOs.Products;

public class ProductQueryParameters
{
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
}
