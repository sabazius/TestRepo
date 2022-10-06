using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BookStore.Host.HealthChecks
{
	internal class CustomHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                //throw new Exception("Random Error Caught!");
                return Task.FromResult(HealthCheckResult.Healthy("OK"));
            }
            catch (Exception ex)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy(ex.Message));
            }
        }
    }
}
