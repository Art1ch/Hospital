using AppointmentAPI.Application.Contracts.Repository.Appointment;
using AppointmentAPI.Application.Contracts.UnitOfWork;
using AppointmentAPI.Infrastructure.BackgroundWorkers;
using AppointmentAPI.Infrastructure.DataInitializers;
using AppointmentAPI.Infrastructure.Implementations;
using AppointmentAPI.Infrastructure.Interfaces;
using AppointmentAPI.Infrastructure.Services.Repository;
using AppointmentAPI.Infrastructure.Services.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace AppointmentAPI.Infrastructure;

public static class InfrastructureInjection
{
    public static void AddInfrastructureLayer(this IServiceCollection services, string connectionString)
    {
        AddDbConnection(services, connectionString);
        AddDatabaseInitializer(services);
        AddRepositories(services);
        AddUnitOfWork(services);
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
        services.AddScoped<IEmailSender, EmailSender>();
    }

    private static void AddNotifications(IServiceCollection services)
    {
        services.AddScoped<IAppointmentNotificationService, AppointmentNotificationService>();
    }

    private static void AddBackgroundWorkers(IServiceCollection services)
    {
        services.AddHostedService<AppointmentReminderWorker>();
    }
}
