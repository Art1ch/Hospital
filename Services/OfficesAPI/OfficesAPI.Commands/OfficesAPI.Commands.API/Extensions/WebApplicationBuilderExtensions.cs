using OfficesAPI.Commands.Infrastructure.Settings;
using OfficesAPI.Shared.Settings;

namespace OfficesAPI.Commands.API.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static MessageBrokerSettings ConfigureMessageBroker(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(MessageBrokerSettings);
        var settings = builder.Configuration.GetSection(sectionName).Get<MessageBrokerSettings>()!;
        builder.Services.Configure<MessageBrokerSettings>(builder.Configuration.GetSection(sectionName));
        return settings;
    }

    public static EventStoreSettings ConfigureEventStore(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(EventStoreSettings);
        var settings = builder.Configuration.GetSection(sectionName).Get<EventStoreSettings>()!;
        builder.Services.Configure<EventStoreSettings>(builder.Configuration.GetSection(sectionName));
        return settings;
    }

    public static CloudinarySettings ConfigureCloudinary(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(CloudinarySettings);
        var settings = builder.Configuration.GetSection(sectionName).Get<CloudinarySettings>()!;
        builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection(sectionName));
        return settings;
    }
}
