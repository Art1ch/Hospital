using AppointmentAPI.Application.Contracts.Email;
using AppointmentAPI.Application.Contracts.RemoteCaller;
using AppointmentAPI.Application.Contracts.Repository.Appointment;
using AppointmentAPI.Application.Contracts.UnitOfWork;
using AppointmentAPI.Infrastructure.DataInitializers;
using AppointmentAPI.Infrastructure.Services.Email;
using AppointmentAPI.Infrastructure.Services.RemoteCaller;
using AppointmentAPI.Infrastructure.Services.Repository;
using AppointmentAPI.Infrastructure.Services.UnitOfWork;
using AppointmentAPI.Infrastructure.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Shared.Protos.Auth;
using Shared.Protos.Doctor;
using System.Data;

namespace AppointmentAPI.Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        string connectionString,
        GrpcSettings grpcSettings
    )
    {
        services.AddDbConnection(connectionString);
        services.AddDatabaseInitializer();
        services.AddRepositories();
        services.AddUnitOfWork();
        services.AddEmail();
        services.AddGrpcServices(grpcSettings);
        services.AddRemoteCaller();

        return services;
    }

    private static IServiceCollection AddDbConnection(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IDbConnection>(_ =>
            new SqlConnection(connectionString));
        return services;
    }

    private static IServiceCollection AddDatabaseInitializer(this IServiceCollection services)
    {
        services.AddTransient<IStartupFilter, DatabaseInitializer>();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        return services;

    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    private static IServiceCollection AddEmail(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();
        return services;
    }

    private static IServiceCollection AddGrpcServices(this IServiceCollection services, GrpcSettings grpcSettings)
    {
        services.AddGrpcClient<DoctorService.DoctorServiceClient>(options =>
        {
            options.Address = new Uri(grpcSettings.DoctorServiceAddress);
        });

        services.AddGrpcClient<AuthService.AuthServiceClient>(options =>
        {
            options.Address = new Uri(grpcSettings.AuthServiceAddress);
        });

        return services;
    }

    private static IServiceCollection AddRemoteCaller(this IServiceCollection services)
    {
        services.AddTransient<IRemoteCaller, RemoteCaller>();
        return services;
    }
}
