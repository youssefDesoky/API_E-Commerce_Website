using System;
using System.Text.Json.Serialization;

namespace E_Commerce.Shared.DTOs.Products;

public class ProductQueryParameters
{
    private const int DEFAULTPAGESIZE = 6;
    private const int MAXPAGESIZE = 10;
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public string? SearchTerm { get; set; }
    public ProductSortOption? SortOption { get; set; }

    public int PageIndex { get; set; } = 1;
    private int _pageSize = DEFAULTPAGESIZE;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MAXPAGESIZE) ? MAXPAGESIZE : (value < DEFAULTPAGESIZE) ? DEFAULTPAGESIZE : value;
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductSortOption
{
    NameAsc,
    NameDesc,
    PriceAsc,
    PriceDesc
}