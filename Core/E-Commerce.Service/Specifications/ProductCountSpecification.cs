using System;
using System.Linq.Expressions;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.DTOs.Products;

namespace E_Commerce.Service.Specifications;

public class ProductCountSpecification : BaseSpecification<Product>
{
    public ProductCountSpecification(ProductQueryParameters parameters) : base(BuildCriteria(parameters))
    {

    }
    
    private static Expression<Func<Product, bool>> BuildCriteria(ProductQueryParameters parameters)
    {
        return p =>
            (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId.Value) &&
            (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId.Value)
            && (string.IsNullOrEmpty(parameters.SearchTerm) || p.Name.Contains(parameters.SearchTerm));
    }
}
