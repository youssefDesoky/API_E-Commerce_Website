using System;
using System.Text.Json;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;
using StackExchange.Redis;

namespace E_Commerce.Persistence.Repositories;

public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
{
    private readonly IDatabase _database = connection.GetDatabase();
    
    public async Task<CustomerBasket?> GetBasketAsync(string basketId)
    {
        var redisValue = await _database.StringGetAsync(basketId);
        if (redisValue.IsNullOrEmpty) return null;

        var customerBasket = JsonSerializer.Deserialize<CustomerBasket>(redisValue!);
        if (customerBasket == null) return null;
        
        return customerBasket;
    }

    public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan duration)
    {
        var basketSerialized = JsonSerializer.Serialize(basket);

        var created = await _database.StringSetAsync(basket.Id, basketSerialized, duration);
        if (!created) return null;

        return basket;
    }

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
        var isDeleted = await _database.KeyDeleteAsync(basketId);
        return isDeleted;
    }
}
