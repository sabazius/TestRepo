using System.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BookStore.Host.HealthChecks
{
    internal class MySqlHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;
        public MySqlHealthCheck(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            await using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);
                }
                catch (SqlException e)
                {
                    return HealthCheckResult.Unhealthy(e.Message);
                }
            }

            return HealthCheckResult.Healthy("SQL connection is OK");
        }
    }
}
