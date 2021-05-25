using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Persistence.DataContext;
using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence.Services;
using MediatR;
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
				//.AddServices("albums")
                .AddInfrastructureServices()
                .AddLogging(b => b.AddConsole())
				.AddMediatR(Assembly.GetExecutingAssembly());

                services.AddTransient<Worker>();
                

                var serviceProvider = services.BuildServiceProvider();
                
                var worker = serviceProvider.GetService<Worker>();

                var logger = serviceProvider.GetService<ILogger<Program>>();
                logger.LogInformation("Starting Application!");

                worker.Run(CancellationToken.None);
            }
            catch (System.Exception ex)
            {
                 // TODO
            }
            
        }
    }
}
