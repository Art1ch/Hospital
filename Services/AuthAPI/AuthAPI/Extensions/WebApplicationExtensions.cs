using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace AuthAPI.Extensions;

public static class WebApplicationExtensions
{
    private const string RussianCulture = "ru";
    private const string EnglishCulture = "en";

    public static void UseLocalization(this WebApplication app)
    {
        app.UseRequestLocalization(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo(EnglishCulture), 
                new CultureInfo(RussianCulture)
            };
            options.DefaultRequestCulture = new RequestCulture(EnglishCulture);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
    }
}
