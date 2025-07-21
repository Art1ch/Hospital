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
using System.Data.Common;

namespace AppointmentAPI.Infrastructure;

public static class InfrastructureInjection
{
    public static void AddInfrastructureLayer(this IServiceCollection services, string connectionString, GrpcSettings grpcSettings)
    {
        AddDbConnection(services, connectionString);
        AddDatabaseInitializer(services);
        AddRepositories(services);
        AddUnitOfWork(services);
        AddEmail(services);
        AddGrpcServices(services, grpcSettings);
        AddRemoteCaller(services);
    }

    private static void AddDbConnection(IServiceCollection services, string connectionString)
    {
        services.AddScoped<DbConnection>(_ =>
            new SqlConnection(connectionString));
    }

    private static void AddDatabaseInitializer(IServiceCollection services)
    {
        services.AddTransient<IStartupFilter, DatabaseInitializer>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
    }

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddEmail(IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
    }

    private static void AddGrpcServices(IServiceCollection services, GrpcSettings grpcSettings)
    {
        services.AddGrpcClient<DoctorService.DoctorServiceClient>(options =>
        {
            options.Address = new Uri(grpcSettings.DoctorServiceAddress);
        });

        services.AddGrpcClient<AuthService.AuthServiceClient>(options =>
        {
            options.Address = new Uri(grpcSettings.AuthServiceAddress);
        });
    }

    private static void AddRemoteCaller(IServiceCollection services)
    {
        services.AddScoped<IRemoteCaller, RemoteCaller>();
    }
}
