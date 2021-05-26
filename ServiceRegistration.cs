using System;
using Infrastructure.Persistence.DataContext;
using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataIngestion.TestAssignment
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Data Source=127.0.0.1,1433;Initial Catalog=IngestionDb;User Id=sa;Password=sa;"));
            services.AddSingleton<IDrillData, DrillDataService>();
            services.AddScoped<IMusicCollectionRepository, MusicCollectionRepository>();
            services.AddScoped<IPopulate, PopulateService>();

            return services;
        }
    }
}