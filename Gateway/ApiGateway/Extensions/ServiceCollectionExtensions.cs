using Ocelot.Settings;

namespace Ocelot.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, CorsSettings corsSettings)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(corsSettings.PolicyName,
                builder =>
                {
                    builder.WithOrigins(corsSettings.Origin)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        return services;
    }

    public static IServiceCollection AddOfficeGatewayHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient("OfficeGateway")
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    AllowAutoRedirect = false
                });

        return services;
    }
}