using OfficesAPI.Commands.API.Healthchecks;

namespace OfficesAPI.Commands.API.Extensions;

internal static class ServiceCollectionExtensions
{
    private const string OfficeHealthCheckName = "office-command-service";

    public static IServiceCollection AddAllHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<OfficeServiceHealthCheck>(OfficeHealthCheckName);

        return services;
    }
}
