using AuthAPI.Configuration.DbSettings;
using AuthAPI.Configuration.JwtSettings;

namespace AuthAPI.Extensions;

internal static class WebApplicationBuilderExtensions
{
    private const string ResourcesPath = "Resources";

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

    public static void AddResourcePathForLocalization(this WebApplicationBuilder builder)
    {
        builder.Services.AddLocalization(options => options.ResourcesPath = ResourcesPath);
    }
}