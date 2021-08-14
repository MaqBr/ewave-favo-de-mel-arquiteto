using System.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FavoDeMel.WebStatus.Extensions
{
    public class SqlServerVendaDbHealthCheck : IHealthCheck
    {
        readonly string _connection;

        public SqlServerVendaDbHealthCheck(string connection)
        {
            _connection = connection;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                using (var connection = new SqlConnection(_connection))
                {
                    await connection.OpenAsync(cancellationToken);

                    var command = connection.CreateCommand();
                    command.CommandText = "select count(*) from Pedidos";

                    return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0 
                        ? HealthCheckResult.Healthy() 
                        : HealthCheckResult.Unhealthy();
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(ex.Message);
            }
        }
    }
}
