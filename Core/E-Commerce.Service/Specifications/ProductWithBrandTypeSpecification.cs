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
        
        ApplyPaging(parameters.PageSize, parameters.PageIndex);

        switch (parameters.SortOption)
        {
            case ProductSortOption.NameAsc:
                AddOrderBy(p => p.Name);
                break;
            case ProductSortOption.NameDesc:
                AddOrderByDesc(p => p.Name);
                break;
            case ProductSortOption.PriceAsc:
                AddOrderBy(p => p.Price);
                break;
            case ProductSortOption.PriceDesc:
                AddOrderByDesc(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
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
            (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId.Value)
            && (string.IsNullOrEmpty(parameters.SearchTerm) || p.Name.Contains(parameters.SearchTerm));
    }
}
