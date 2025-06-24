using AuthAPI.Configuration.DbSettings;
using AuthAPI.Configuration.JwtSettings;

namespace AuthAPI.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static void ConfigureJwtSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection(nameof(JwtSettings)));
    }

    public static string ConfigureAuthDbSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<AuthDbSettings>(
           builder.Configuration.GetSection(nameof(AuthDbSettings)));

        return builder.Configuration["AuthDbSettings:ConnectionString"]!;
    }
}
