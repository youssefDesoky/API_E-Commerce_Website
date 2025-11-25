using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;
using E_Commerce.Domain.Exceptions.BadRequest;
using E_Commerce.Domain.Exceptions.NotFound;
using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.DTOs.Baskets;

namespace E_Commerce.Service.Services;

public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
{
    public async Task<CustomerBasketDto?> GetBasketAsync(string basketId)
    {
        var basket = await basketRepository.GetBasketAsync(basketId);
        
        if (basket is null) throw new BasketNotFoundException(basketId);

        return mapper.Map<CustomerBasketDto>(basket);
    }

    public async Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto basket, TimeSpan duration)
    {
        var basketEntity = mapper.Map<CustomerBasket>(basket);

        var updatedBasket = await basketRepository.UpdateBasketAsync(basketEntity, duration);
        if (updatedBasket is null) throw new UpdateAndCreateBadRequestException(basket.Id);

        return mapper.Map<CustomerBasketDto>(updatedBasket);
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
        var isDeleted =  await basketRepository.DeleteBasketAsync(basketId);
        if (!isDeleted) throw new DeleteBasketBadRequestException(basketId);

        return isDeleted;
    }
}
