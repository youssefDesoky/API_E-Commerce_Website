using System;
using AutoMapper;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.DTOs.Products;
using Microsoft.Extensions.Configuration;

namespace E_Commerce.Service.MappingProfile.Resolvers;

public class PictureURLResolver(IConfiguration configuration) : IValueResolver<Product, ProductDto, string>
{
    public string Resolve(Product source, ProductDto destination, string? destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.PictureUrl)) return string.Empty;

        return $"{configuration["ApiSettings:BaseUrl"]}{source.PictureUrl}";
    }
}
