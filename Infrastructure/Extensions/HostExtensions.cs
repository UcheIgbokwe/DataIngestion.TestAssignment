using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;

           using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetRequiredService<TContext>();

            try
            {
                logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");

                InvokeSeeder(context);

                logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
            }
            catch (SqlException ex)
            {
                logger.LogError($"An error occuredwhile migrating database used on context {typeof(TContext).Name}, Message: {ex.Message}");

                if (retryForAvailability < 50)
                {
                    retryForAvailability++;
                    System.Threading.Thread.Sleep(2000);
                    MigrateDatabase<TContext>(host, retryForAvailability);
                }
            }
            return host;
        }

        public static void InvokeSeeder<TContext>(TContext context) where TContext : DbContext
        {
            context.Database.Migrate();
        } 
    }
}