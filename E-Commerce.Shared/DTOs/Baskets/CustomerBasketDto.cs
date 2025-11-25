using System;

namespace E_Commerce.Shared.DTOs.Baskets;

public class CustomerBasketDto
{
    public string Id { get; set; }
    public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
}
