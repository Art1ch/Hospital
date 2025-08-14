using AppointmentAPI.Application.Settings;
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

    public static GrpcSettings ConfigureGrpcSettings(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(GrpcSettings);
        var settings = builder.Configuration.GetSection(sectionName).Get<GrpcSettings>()!;
        builder.Services.Configure<GrpcSettings>(builder.Configuration.GetSection(sectionName));
        return settings;
    }

    public static void ConfigureNotificationSettings(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(NotificationSettings);
        builder.Services.Configure<NotificationSettings>(builder.Configuration.GetSection(sectionName));
    }
}
