using DoctorAPI.Application.Contracts.Cache;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Contracts.Repository.Specialization;
using DoctorAPI.Application.Contracts.UnitOfWork;
using DoctorAPI.Infrastructure.Context;
using DoctorAPI.Infrastructure.Repositories.Implementations;
using DoctorAPI.Infrastructure.Services;
using DoctorAPI.Infrastructure.UnitOfWorkImplementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorAPI.Infrastructure;

public static class InfrastructureLayerInjection
{
    public static void AddInfrastructureLayer(this IServiceCollection services, string dbConnectionString)
    {
        AddDbContext(services, dbConnectionString);
        AddRepositories(services);
        AddCaching(services);
        AddUnitOfWork(services);
    }

    private static void AddDbContext(IServiceCollection services, string dbConnectionString)
    {
        services.AddDbContext<DoctorDbContext>(opt =>
            opt.UseNpgsql(dbConnectionString));
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<ISpecializationRepository, SpecializationRepository>();
    }

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddCaching(IServiceCollection services)
    {
        services.AddScoped<ICacheService, CacheService>();
    }
}
