using AuthAPI.Application.Contracts.Repository.Account;
using AuthAPI.Application.Contracts.UnitOfWork;
using AuthAPI.Infrastructure.Context;
using AuthAPI.Infrastructure.Repositories.Implementations;
using AuthAPI.Infrastructure.UnitOfWorkImplementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthAPI.Infrastructure;

public static class Injection
{
    public static void AddInfrastructureLayer(this IServiceCollection services, string dbConnectionString)
    {
        AddDbContext(services, dbConnectionString);
        AddRepositories(services);
        AddUnitOfWork(services);
    }

    private static void AddDbContext(IServiceCollection services, string dbConnectionString)
    {
        services.AddDbContext<AuthDbContext>(opt =>
            opt.UseNpgsql(dbConnectionString));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IReferenceTokenRepository, ReferenceTokenRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
