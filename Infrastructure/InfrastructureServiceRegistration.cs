using Infrastructure.Persistence.DataContext;
using Infrastructure.Persistence.Interface;
using Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Data Source=127.0.0.1,1433;Initial Catalog=IngestionDb;User Id=sa;Password=Ebubechi89;"));
            services.AddSingleton<IDrillData, DrillDataService>();

            return services;
        }
    }
}