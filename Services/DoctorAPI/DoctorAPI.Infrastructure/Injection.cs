using DoctorAPI.Application.Contracts.UnitOfWork;
using DoctorAPI.Infrastructure.Context;
using DoctorAPI.Infrastructure.UnitOfWorkImplementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorAPI.Infrastructure;

public static class Injection
{
    public static void AddInfrastructureLayer(this IServiceCollection services, string dbConnectionString)
    {
        AddDbContext(services, dbConnectionString);
        AddUnitOfWork(services);
    }

    private static void AddDbContext(IServiceCollection services, string dbConnectionString)
    {
        services.AddDbContext<DoctorDbContext>(opt =>
            opt.UseNpgsql(dbConnectionString));
    }

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
