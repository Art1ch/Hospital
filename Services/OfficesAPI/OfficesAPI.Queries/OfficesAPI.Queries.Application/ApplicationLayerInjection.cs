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
    private static readonly Assembly _assembly = typeof(ApplicationLayerInjection).Assembly;

    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, MessageBrokerSettings messageBrokerSettings)
    {
        services.AddValidation().
            AddQueries().
            AddMapping().
            AddPipelineBehavior().
            AddConsumers(messageBrokerSettings);

        return services;
    }


    private static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(_assembly);

        return services;
    }

    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(_assembly);
        });

        return services;
    }

    private static IServiceCollection AddMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(_assembly);

        return services;
    }

    private static IServiceCollection AddPipelineBehavior(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        return services;
    }

    private static IServiceCollection AddConsumers(this IServiceCollection services, MessageBrokerSettings messageBrokerSettings)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumers(_assembly);
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

        return services;
    }
}
