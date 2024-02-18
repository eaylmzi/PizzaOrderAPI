using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PizzaOrder.Data.Models;
using System.Data;
using System.Data.Common;

namespace PizzaOrderAPI.Services.Health
{
    internal sealed class DatabaseHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var _context = new PizzaOrderDBContext())
                {
                    await _context.Database.OpenConnectionAsync(cancellationToken);
                    await _context.Pizzas.AnyAsync(cancellationToken);
                }
                return HealthCheckResult.Healthy("The database connection is successful.");
            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy($"An unexpected error occured {e.Message}");
            }
        }
    }

}
