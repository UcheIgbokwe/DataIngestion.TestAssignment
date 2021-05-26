using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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

                worker.Run(CancellationToken.None);

                #if DEBUG
                    Console.WriteLine("Press enter to close...");
                    Console.ReadLine();
                #endif
            }
            catch (System.Exception ex)
            {
                 // TODO
            }
            
        }
    }
}
