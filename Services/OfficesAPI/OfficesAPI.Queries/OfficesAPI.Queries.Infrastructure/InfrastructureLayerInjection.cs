using Microsoft.Extensions.DependencyInjection;
using OfficesAPI.Queries.Application.Contracts.Repository.Office;
using OfficesAPI.Queries.Application.Contracts.UnitOfWork;
using OfficesAPI.Queries.Infrastructure.Context;
using OfficesAPI.Queries.Infrastructure.Repositories;
using OfficesAPI.Infrastructure.Services;

namespace OfficesAPI.Queries.Infrastructure;

public static class InfrastructureLayerInjection
{
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        AddDbContext(services);
        AddRepositories(services);
        AddUnitOfWork(services);
    }

    private static void AddDbContext(IServiceCollection services)
    {
        services.AddScoped<OfficeDbContext>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IOfficeRepository, OfficeRepository>();
    }

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
