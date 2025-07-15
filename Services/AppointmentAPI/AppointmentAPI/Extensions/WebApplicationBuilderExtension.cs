using AppointmentAPI.Infrastructure.Settings;

namespace AppointmentAPI.Extensions;

public static class WebApplicationBuilderExtension
{
    public static string ConfigureAppointmentDbSetting(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(AppointmentDbSettings);
        var settings = builder.Configuration.GetSection(sectionName).Get<AppointmentDbSettings>()!;
        builder.Services.Configure<AppointmentDbSettings>(builder.Configuration.GetSection(sectionName));
        return settings.ConnectionString;
    }
}
