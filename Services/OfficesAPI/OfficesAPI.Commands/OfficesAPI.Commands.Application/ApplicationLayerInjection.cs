using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OfficesAPI.Commands.Application;

public static class ApplicationLayerInjection
{
    private static readonly Assembly _assembly = typeof(ApplicationLayerInjection).Assembly;

    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddValidation().
            AddCommands().
            AddMapping();

        return services;
    }

    private static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(_assembly);

        return services;
    }

    private static IServiceCollection AddCommands(this IServiceCollection services) =>
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(_assembly);
        });

    private static IServiceCollection AddMapping(this IServiceCollection services) =>
        services.AddAutoMapper(_assembly);
}