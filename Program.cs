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
            var services = new ServiceCollection()
				//.AddServices("albums")
                .AddLogging(b => b.AddConsole())
				.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<Worker>();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Data Source=127.0.0.1,1433;Initial Catalog=IngestionDb;User Id=sa;Password=Ebubechi89;"));
            services.AddSingleton<IDrillData, DrillDataService>();
            services.AddScoped<IMusicCollectionRepository, MusicCollectionRepository>();
            services.AddScoped<IPopulate, PopulateService>();

            var serviceProvider = services.BuildServiceProvider();
            
            var worker = serviceProvider.GetService<Worker>();

            var logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogInformation("Starting Application!");

            worker.Run(CancellationToken.None);
        }
    }
}
