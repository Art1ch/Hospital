using Microsoft.Extensions.DependencyInjection;
using OfficesAPI.Commands.Application.Contracts;
using OfficesAPI.Queries.Application.Contracts.Repository.Office;
using OfficesAPI.Queries.Application.Contracts.UnitOfWork;
using OfficesAPI.Shared.Infrastructure.Context;
using OfficesAPI.Shared.Infrastructure.Repository;
using OfficesAPI.Shared.Infrastructure.Services;
using OfficesAPI.Shared.Infrastructure.Settings;

namespace OfficesAPI.Shared.Infrastructure;

public static class SharedInfrastructureInjection
{
    public static IServiceCollection AddWriteRepository(this IServiceCollection services, OfficeDbSettings settings)
    {
        services.AddDbContext(settings);
        services.AddScoped<ICommandOfficeRepository, OfficeRepository>();
        return services;
    }

    public static IServiceCollection AddReadRepository(this IServiceCollection services, OfficeDbSettings settings)
    {
        services.AddDbContext(settings);
        services.AddScoped<IQueryOfficeRepository, OfficeRepository>();
        return services;
    }

    public static IServiceCollection AddUnitOfWork(this IServiceCollection services) =>
        services.AddScoped<IUnitOfWork, UnitOfWork>();

    private static IServiceCollection AddDbContext(this IServiceCollection services, OfficeDbSettings settings)
    {
        services.AddScoped(x =>
        {
            return new OfficeDbContext(settings);
        });

        return services;
    }
}