using AppointmentAPI.Application.PipelineBehavior;
using FluentValidation;
using FluentValidation.AspNetCore;
using AppointmentAPI.Application.Contracts.AppointmentNotificationService;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AppointmentAPI.Application.Implementations;

namespace AppointmentAPI.Application;

public static class ApplicationLayerInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationLayerInjection).Assembly;

        services.AddValidation(assembly);
        services.AddCommandsAndQueries(assembly);
        services.AddMapping(assembly);
        services.AddPipelineBehavior();
        services.AddNotificationService();

        return services;
    }
    private static IServiceCollection AddValidation(this IServiceCollection services, Assembly assembly)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(assembly);
        return services;
    }

    private static IServiceCollection AddCommandsAndQueries(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(assembly);
        });
        return services;
    }

    private static IServiceCollection AddMapping(this IServiceCollection services, Assembly assembly)
    {
        services.AddAutoMapper(assembly);
        return services;
    }

    private static IServiceCollection AddPipelineBehavior(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        return services;
    }

    private static IServiceCollection AddNotificationService(this IServiceCollection services)
    {
        services.AddScoped<IAppointmentNotificationService, AppointmentNotificationService>();
        return services;
    }
}
