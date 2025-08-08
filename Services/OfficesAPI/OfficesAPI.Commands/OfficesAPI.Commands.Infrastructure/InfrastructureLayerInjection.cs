using EventStore.Client;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using OfficesAPI.Commands.Application.Contracts;
using OfficesAPI.Commands.Infrastructure.Services;
using OfficesAPI.Commands.Infrastructure.Settings;
using OfficesAPI.Shared.Settings;

namespace OfficesAPI.Infrastructure;

public static class InfrastructureLayerInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        EventStoreSettings eventStoreSettings,
        MessageBrokerSettings messageBrokerSettings
    )
    {
        services.AddEventStore(eventStoreSettings).
            AddMessageBroker(messageBrokerSettings).
            AddMessagePublisher();

        return services;
    }


    private static IServiceCollection AddEventStore(this IServiceCollection services, EventStoreSettings settings)
    {
        services.AddSingleton(sp =>
        {
            var options = EventStoreClientSettings.Create(settings.ConnectionString);
            return new EventStoreClient(options);
        });

        services.AddScoped(typeof(IEventStore<>), typeof(EventStore<>));

        return services;
    }

    private static IServiceCollection AddMessageBroker(this IServiceCollection services, MessageBrokerSettings messageBrokerSettings)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(messageBrokerSettings.Hostname, messageBrokerSettings.VirtualHost, h =>
                {
                    h.Username(messageBrokerSettings.Username);
                    h.Password(messageBrokerSettings.Password);
                });
            });
        });

        return services;
    }

    private static IServiceCollection AddMessagePublisher(this IServiceCollection services)
    {
        services.AddScoped<IMessagePublisher, MessagePublisher>();

        return services;
    }
}
