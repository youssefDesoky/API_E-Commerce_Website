using E_Commerce.Domain.Entities.Baskets;

namespace E_Commerce.Domain.Contracts;

public interface IBasketRepository
{
    Task<CustomerBasket> GetBasketAsync(string basketId);
    Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket); // Create Or Update
    Task<bool> DeleteBasketAsync(string basketId);
}
