using Microsoft.Extensions.DependencyInjection;
using OfficesAPI.Queries.Application.Contracts.Repository.Office;
using OfficesAPI.Queries.Application.Contracts.UnitOfWork;
using OfficesAPI.Queries.Infrastructure.Context;
using OfficesAPI.Queries.Infrastructure.Repositories;
using OfficesAPI.Infrastructure.Services;

namespace OfficesAPI.Queries.Infrastructure;

public static class InfrastructureLayerInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddDbContext().
            AddRepositories().
            AddUnitOfWork();

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services) =>
        services.AddScoped<OfficeDbContext>();

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services.AddScoped<IOfficeRepository, OfficeRepository>();

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services) =>
        services.AddScoped<IUnitOfWork, UnitOfWork>();
}
