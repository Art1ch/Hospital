using OfficesAPI.Shared.Infrastructure.Settings;
using OfficesAPI.Shared.Settings;

namespace OfficeAPI.Queries.API.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static OfficeDbSettings ConfigureDbSettings(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(OfficeDbSettings);
        var settings = builder.Configuration.GetSection(sectionName).Get<OfficeDbSettings>()!;
        builder.Services.Configure<OfficeDbSettings>(builder.Configuration.GetSection(sectionName));
        return settings;
    }

    public static MessageBrokerSettings ConfigureMessageBroker(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(MessageBrokerSettings);
        var settings = builder.Configuration.GetSection(sectionName).Get<MessageBrokerSettings>()!;
        return settings;
    }
}
