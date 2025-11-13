using System;
using AutoMapper;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Service.MappingProfile.Resolvers;
using E_Commerce.Shared.DTOs.Products;
using Microsoft.Extensions.Configuration;

namespace E_Commerce.Service.MappingProfile;

public class ProductProfile : Profile
{
    private readonly IConfiguration _configuration;
    public ProductProfile(IConfiguration configuration)
    {
        CreateMap<ProductBrand, BrandDto>();
        CreateMap<ProductType, TypeDto>();
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.ProductBrand.Name))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ProductType.Name))
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(new PictureURLResolver(configuration)));
    }
}
