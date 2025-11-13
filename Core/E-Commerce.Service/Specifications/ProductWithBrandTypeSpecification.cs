using System;
using System.Linq.Expressions;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.DTOs.Products;

namespace E_Commerce.Service.Specifications;

public class ProductWithBrandTypeSpecification : BaseSpecification<Product>
{
    public ProductWithBrandTypeSpecification(ProductQueryParameters parameters)
        : base(BuildCriteria(parameters))
    {
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
    }

    public ProductWithBrandTypeSpecification(int id)
        : base(p => p.Id == id)
    {
        AddInclude(p => p.ProductBrand);
        AddInclude(p => p.ProductType);
    }

    private static Expression<Func<Product, bool>> BuildCriteria(ProductQueryParameters parameters)
    {
        return p =>
            (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId.Value) &&
            (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId.Value);
    }
}
