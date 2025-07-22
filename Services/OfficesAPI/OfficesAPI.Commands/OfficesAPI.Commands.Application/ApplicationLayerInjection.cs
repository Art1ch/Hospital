using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OfficesAPI.Commands.Application;

public static class ApplicationLayerInjection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationLayerInjection).Assembly;

        AddValidation(services, assembly);
        AddCommands(services, assembly);
        AddMapping(services, assembly);
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
}