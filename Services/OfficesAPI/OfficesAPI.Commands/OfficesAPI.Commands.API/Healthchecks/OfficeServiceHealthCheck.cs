using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OfficesAPI.Commands.API.Healthchecks;

public class OfficeServiceHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }
        catch (Exception exception)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy(exception: exception));
        }
    }
}
