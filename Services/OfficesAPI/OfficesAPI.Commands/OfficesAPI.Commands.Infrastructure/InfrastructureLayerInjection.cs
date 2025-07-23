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
    public static void AddInfrastructureLayer(
        this IServiceCollection services,
        EventStoreSettings eventStoreSettings,
        MessageBrokerSettings messageBrokerSettings
    )
    {
        AddEventStore(services, eventStoreSettings);
        AddMessageBroker(services, messageBrokerSettings);
    }

    private static void AddEventStore(this IServiceCollection services, EventStoreSettings settings)
    {
        services.AddSingleton(sp => 
        {
            var options = EventStoreClientSettings.Create(settings.ConnectionString);
            return new EventStoreClient(options);
        });

        services.AddScoped(typeof(IEventStore<>), typeof(EventStore<>));
    } 

    private static void AddMessageBroker(this IServiceCollection services, MessageBrokerSettings messageBrokerSettings)
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
    }
}
