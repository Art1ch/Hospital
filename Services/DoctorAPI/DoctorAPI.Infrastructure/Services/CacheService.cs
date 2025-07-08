using DoctorAPI.Application.Contracts.Cache;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DoctorAPI.Infrastructure.Services;

internal class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;         
    }

    public async Task<TValue?> GetAsync<TValue>(string key, CancellationToken cancellationToken = default)
    {
        var stringValue = await _distributedCache.GetStringAsync(key, cancellationToken);
        if (stringValue != null)
        {
            var value = JsonSerializer.Deserialize<TValue>(stringValue);
            return value;
        }
        return default(TValue);
    }

    public async Task SetAsync<TValue>(string key, TValue value, CancellationToken cancellationToken = default)
    {
        var stringValue = JsonSerializer.Serialize<TValue>(value);
        await _distributedCache.SetStringAsync(key, stringValue, cancellationToken);
    }

    public async Task RefreshAsync(string key, CancellationToken cancellationToken = default)
    {
        await _distributedCache.RefreshAsync(key, cancellationToken);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _distributedCache.RemoveAsync(key, cancellationToken);
    }
}
