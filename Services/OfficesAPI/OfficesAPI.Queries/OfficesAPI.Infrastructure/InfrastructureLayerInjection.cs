using Microsoft.Extensions.DependencyInjection;
using OfficesAPI.Application.Contracts.Repository.Office;
using OfficesAPI.Application.Contracts.UnitOfWork;
using OfficesAPI.Infrastructure.Context;
using OfficesAPI.Infrastructure.Repositories;
using OfficesAPI.Infrastructure.Services;

namespace OfficesAPI.Infrastructure;

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
