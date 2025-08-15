using DoctorAPI.Application.Contracts.Cache;
using DoctorAPI.Caching.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorAPI.Caching;

public static class CacheInjection
{
    public static void AddCacheService(this IServiceCollection services)
    {
        services.AddScoped<ICacheService, CacheService>();
    }
}
