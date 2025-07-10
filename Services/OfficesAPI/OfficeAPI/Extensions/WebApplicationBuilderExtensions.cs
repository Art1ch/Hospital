using OfficesAPI.Infrastructure.Settings;

namespace OfficeAPI.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static void ConfigureDbSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<OfficeDbSettings>(
            builder.Configuration.GetSection(nameof(OfficeDbSettings)));
    } 
}
