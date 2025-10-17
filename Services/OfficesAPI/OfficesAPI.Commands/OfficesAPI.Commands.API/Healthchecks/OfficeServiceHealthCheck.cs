using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OfficesAPI.Commands.API.Healthchecks;

public class OfficeServiceHealthCheck : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            return await Task.FromResult(HealthCheckResult.Healthy());
        }
        catch (Exception exception)
        {
            return await Task.FromResult(HealthCheckResult.Unhealthy(exception: exception));
        }
    }
}
