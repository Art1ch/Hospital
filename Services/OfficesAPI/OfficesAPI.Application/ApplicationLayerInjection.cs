using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OfficesAPI.Application.PipelineBehavior;
using System.Reflection;

namespace OfficesAPI.Application;

public static class ApplicationLayerInjection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationLayerInjection).Assembly;

        AddValidation(services, assembly);
        AddCommands(services, assembly);
        AddMapping(services, assembly);
        AddPipelineBehavior(services);
    }

    private static void AddValidation(IServiceCollection services, Assembly assembly)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(assembly);
    }

    private static void AddCommands(IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(assembly);
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
