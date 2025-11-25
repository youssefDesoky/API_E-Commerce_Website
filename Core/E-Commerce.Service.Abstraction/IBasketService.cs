using System;
using E_Commerce.Shared.DTOs.Baskets;

namespace E_Commerce.Service.Abstraction;

public interface IBasketService
{
    Task<CustomerBasketDto?> GetBasketAsync(string basketId);
    Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto basket, TimeSpan duration); // Create Or Update
    Task<bool> DeleteBasketAsync(string basketId);
}
