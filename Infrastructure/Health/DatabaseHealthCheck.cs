using Dapper;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace Infrastructure.Health;

public class DatabaseHealthCheck(string connectionString) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync(cancellationToken);
            var result = await connection.ExecuteScalarAsync<int>("SELECT 1");
            if (result == 1)
            {
                return HealthCheckResult.Healthy("Database is reachable.");
            }
            else
            {
                return HealthCheckResult.Unhealthy("Database returned unexpected result.");
            }
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Database is unreachable.", ex);
        }
    }
}