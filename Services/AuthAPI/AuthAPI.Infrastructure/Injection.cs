using AuthAPI.Application.Contracts.PasswordHasher;
using AuthAPI.Application.Contracts.Repository.Account;
using AuthAPI.Application.Contracts.TokenProvider;
using AuthAPI.Application.Contracts.UnitOfWork;
using AuthAPI.Infrastructure.Context;
using AuthAPI.Infrastructure.Implemenations.PasswordHasherImplmentation;
using AuthAPI.Infrastructure.Implemenations.TokenProviderImplementation;
using AuthAPI.Infrastructure.Implemenations.UnitOfWorkImplementation;
using AuthAPI.Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthAPI.Infrastructure;

public static class Injection
{
    public static void AddInfrastructureLayer(this IServiceCollection services, string dbConnectionString)
    {
        AddDbContext(services, dbConnectionString);
        AddRepositories(services);
        AddPasswordHasher(services);
        AddTokenProvider(services);
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

    private static void AddPasswordHasher(IServiceCollection services)
    {
        services.AddTransient<IPasswordHasher, PasswordHasher>();
    } 

    private static void AddTokenProvider(IServiceCollection services)
    {
        services.AddScoped<ITokenProvider, TokenProvider>();
    }
}
