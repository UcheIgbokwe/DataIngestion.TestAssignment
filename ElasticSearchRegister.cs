using System;
using Domain.Model;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace DataIngestion.TestAssignment
{
    public static class ElasticSearchRegister
    {
        public static IServiceCollection AddElasticServices(this IServiceCollection services)
        {
            var url = "http://localhost:9200/";
            var defaultIndex = "albums";

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex)
                .DefaultMappingFor<Album>(x => x.IndexName(defaultIndex).Ignore(p => p.ArtistId));

            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
            services.AddSingleton<ElasticClient>(client);

            client.Indices.Create(defaultIndex, idx => idx.Map<Album>(x => x.AutoMap()));

            return services;
        }
    }
}