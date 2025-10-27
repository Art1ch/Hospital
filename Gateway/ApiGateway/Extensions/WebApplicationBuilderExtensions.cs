using ApiGateway.Settings;
using Ocelot.Settings;

namespace Ocelot.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static CorsSettings ConfigureCors(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(CorsSettings);
        var settings = builder.Configuration.GetSection(sectionName).Get<CorsSettings>()!;
        builder.Services.Configure<CorsSettings>(builder.Configuration.GetSection(sectionName));
        return settings;
    }

    public static OfficeServiceAddressesSettings ConfigureOfficeServiceAddresses(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(OfficeServiceAddressesSettings);
        var settings = builder.Configuration.GetSection(sectionName).Get<OfficeServiceAddressesSettings>()!;
        builder.Services.Configure<OfficeServiceAddressesSettings>(builder.Configuration.GetSection(sectionName));
        return settings;
    }
}