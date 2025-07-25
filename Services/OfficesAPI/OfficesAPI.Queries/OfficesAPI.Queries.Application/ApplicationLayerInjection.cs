using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OfficesAPI.Queries.Application.PipelineBehavior;
using OfficesAPI.Shared.Settings;
using System.Reflection;

namespace OfficesAPI.Queries.Application;

public static class ApplicationLayerInjection
{
    public static void AddApplicationLayer(this IServiceCollection services, MessageBrokerSettings messageBrokerSettings)
    {
        var assembly = typeof(ApplicationLayerInjection).Assembly;

        AddValidation(services, assembly);
        AddQueries(services, assembly);
        AddMapping(services, assembly);
        AddPipelineBehavior(services);
        AddConsumers(services, assembly, messageBrokerSettings);
    }


    private static void AddValidation(IServiceCollection services, Assembly assembly)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(assembly);
    }

    private static void AddQueries(IServiceCollection services, Assembly assembly)
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

    private static void AddConsumers(IServiceCollection services, Assembly assembly, MessageBrokerSettings messageBrokerSettings)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumers(assembly);
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(messageBrokerSettings.Hostname, messageBrokerSettings.VirtualHost, h =>
                {
                    h.Username(messageBrokerSettings.Username);
                    h.Password(messageBrokerSettings.Password);
                });

                cfg.ConfigureEndpoints(context);
            });

        });
    }
}
