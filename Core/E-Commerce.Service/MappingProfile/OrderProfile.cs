using System;
using AutoMapper;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Shared.DTOs.Orders;

namespace E_Commerce.Service.MappingProfile;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<ShippingAddress, OrderAddressDto>();

        CreateMap<Order, OrderResponse>()
            .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.Total, o => o.MapFrom(s => s.GetTotal()));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductId, oi => oi.MapFrom(s => s.ProductItemOrdered.ProductId))
            .ForMember(d => d.ProductName, oi => oi.MapFrom(s => s.ProductItemOrdered.ProductName))
            .ForMember(d => d.PictureUrl, oi => oi.MapFrom(s => s.ProductItemOrdered.PictureUrl));
            
        CreateMap<DeliveryMethod, DeliveryMethodResponse>();
    }
}
