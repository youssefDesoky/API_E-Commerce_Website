using System;
using AutoMapper;
using E_Commerce.Domain.Entities.Baskets;
using E_Commerce.Shared.DTOs.Baskets;

namespace E_Commerce.Service.MappingProfile;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
        CreateMap<BasketItem, BasketItemDto>().ReverseMap();   
    }
}
