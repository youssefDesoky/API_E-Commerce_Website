using System;

namespace E_Commerce.Domain.Entities.Baskets;

public class CustomerBasket
{
    public string Id { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
}
