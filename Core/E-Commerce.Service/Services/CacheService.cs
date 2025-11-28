using System;
using E_Commerce.Domain.Contracts;
using E_Commerce.Service.Abstraction;

namespace E_Commerce.Service.Services;

public class CacheService(ICacheRepository cacheRepository) : ICacheService
{
    public Task<string?> GetAsync(string key)
    {
        return cacheRepository.GetAsync(key);
    }

    public Task SetAsync(string key, object value, TimeSpan duration)
    {
        return cacheRepository.SetAsync(key, value, duration);
    }
}
