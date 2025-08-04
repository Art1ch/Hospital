using DoctorAPI.Caching.Settings;
using DoctorAPI.Infrastructure.Settings;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace DoctorAPI.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static JwtSettings ConfigureJwtSettings(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(JwtSettings);
        var settings = builder.Configuration.GetSection(sectionName).Get<JwtSettings>()!;
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(sectionName));
        return settings;
    }

    public static string ConfigureDoctorDbSettings(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(DoctorDbSettings);
        var settings = builder.Configuration.GetSection(sectionName).Get<DoctorDbSettings>()!;
        builder.Services.Configure<DoctorDbSettings>(builder.Configuration.GetSection(sectionName));
        return settings.ConnectionString;
    }

    public static CacheSettings ConfigureCacheSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<CacheSettings>(
            builder.Configuration.GetSection(nameof(CacheSettings)));

        return builder.Configuration.Get<CacheSettings>()!;
    }
    
    public static void ConfigureWebHostKestrel(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(80, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1;
            });

            options.ListenAnyIP(5000, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        });
    }
}