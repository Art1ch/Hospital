using AuthAPI.Configuration.DbSettings;
using AuthAPI.Configuration.JwtSettings;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace AuthAPI.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static void ConfigureJwtSettings(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(JwtSettings);
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(sectionName));
    }

    public static string ConfigureAuthDbSettings(this WebApplicationBuilder builder)
    {
        var sectionName = nameof(AuthDbSettings);
        var settings = builder.Configuration.GetSection(sectionName).Get<AuthDbSettings>()!;
        builder.Services.Configure<AuthDbSettings>(builder.Configuration.GetSection(sectionName));
        return settings.ConnectionString;
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