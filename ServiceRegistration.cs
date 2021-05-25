using System;
using Domain.Model;
using Infrastructure.Persistence.DataContext;
using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace DataIngestion.TestAssignment
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Data Source=127.0.0.1,1433;Initial Catalog=IngestionDb;User Id=sa;Password=Ebubechi89;"));
            services.AddSingleton<IDrillData, DrillDataService>();
            services.AddScoped<IMusicCollectionRepository, MusicCollectionRepository>();
            services.AddScoped<IPopulate, PopulateService>();

            var url = "http://localhost:9200/";
            var defaultIndex = "albums";

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex)
                .DefaultMappingFor<Album>(x => x.IndexName(defaultIndex));

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

            client.Indices.Create(defaultIndex, idx => idx.Map<Album>(x => x.AutoMap()));

            return services;
        }
    }
}