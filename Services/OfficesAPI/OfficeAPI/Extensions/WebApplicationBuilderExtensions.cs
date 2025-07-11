using OfficesAPI.Infrastructure.Settings;

namespace OfficeAPI.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static void ConfigureDbSettings(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(OfficeDbSettings);
        builder.Services.Configure<OfficeDbSettings>(builder.Configuration.GetSection(sectionName));
    } 
}
