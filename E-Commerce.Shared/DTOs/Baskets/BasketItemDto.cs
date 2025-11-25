using System;

namespace E_Commerce.Shared.DTOs.Baskets;

public class BasketItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PictureUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
