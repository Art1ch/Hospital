using AppointmentAPI.Application.PipelineBehavior;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AppointmentAPI.Application;

public static class ApplicationLayerInjection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationLayerInjection).Assembly;

        AddValidation(services, assembly);
        AddCommandsAndQueries(services, assembly);
        AddMapping(services, assembly);
        AddPipelineBehavior(services);
    }
    private static void AddValidation(IServiceCollection services, Assembly assembly)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(assembly);
    }

    private static void AddCommandsAndQueries(IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(assembly);
        });
    }

    private static void AddMapping(IServiceCollection services, Assembly assembly)
    {
        services.AddAutoMapper(assembly);
    }

    private static void AddPipelineBehavior(IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
    }
}
