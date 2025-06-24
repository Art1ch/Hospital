using DoctorAPI.Infrastructure.Settings;

namespace DoctorAPI.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static JwtSettings ConfigureJwtSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection(nameof(JwtSettings)));

        return builder.Configuration.Get<JwtSettings>()!;
    }

    public static string ConfigureDoctorDbSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<DoctorDbSettings>(
            builder.Configuration.GetSection(nameof(DoctorDbSettings)));

        return builder.Configuration["AuthDbSettings:ConnectionString"]!;
    }
}
