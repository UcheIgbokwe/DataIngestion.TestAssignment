using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Infrastructure.Persistence.DataContext;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataIngestion.TestAssignment
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var services = new ServiceCollection()
                .AddElasticServices()
                .AddInfrastructureServices()
                .AddLogging(b => b.AddConsole())
				.AddMediatR(Assembly.GetExecutingAssembly());

                services.AddTransient<Worker>();
                

                var serviceProvider = services.BuildServiceProvider();

                var worker = serviceProvider.GetService<Worker>();

                var logger = serviceProvider.GetService<ILogger<Program>>();
                logger.LogInformation("Starting Application!");

                try
                {
                    logger.LogInformation($"Migrating database associated with context {typeof(AppDbContext).Name}");
                    var db = serviceProvider.GetRequiredService<AppDbContext>();
                    db.Database.Migrate();
                    logger.LogInformation($"Migrated database associated with context {typeof(AppDbContext).Name}");
                }
                catch (SqlException ex)
                {
                    logger.LogError($"An error occuredwhile migrating database used on context {typeof(AppDbContext).Name}, Message: {ex.Message}");
                }

                worker.Run(CancellationToken.None);

                #if DEBUG
                    Console.WriteLine("Press enter to close...");
                    Console.ReadLine();
                #endif
            }
            catch (System.Exception ex)
            {

            }
            
        }

        
    }
}
