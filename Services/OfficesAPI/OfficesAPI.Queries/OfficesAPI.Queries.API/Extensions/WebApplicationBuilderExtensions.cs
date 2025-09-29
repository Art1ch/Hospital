using OfficesAPI.Queries.Infrastructure.Settings;
using OfficesAPI.Shared.Settings;

namespace OfficeAPI.Queries.API.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static void ConfigureDbSettings(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(OfficeDbSettings);
        builder.Services.Configure<OfficeDbSettings>(builder.Configuration.GetSection(sectionName));
    }

    public static MessageBrokerSettings ConfigureMessageBroker(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(MessageBrokerSettings);
        var settings = builder.Configuration.GetSection(sectionName).Get<MessageBrokerSettings>()!;
        builder.Services.Configure<MessageBrokerSettings>(builder.Configuration.GetSection(sectionName));
        return settings;
    }
}
