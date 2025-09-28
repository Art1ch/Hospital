using OfficesAPI.Shared.Settings;

namespace OfficesAPI.Queries.API.Extensions;

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
}
