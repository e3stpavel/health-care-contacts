using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UtterlyComplete.ApplicationCore.Interfaces.Repositories;
using UtterlyComplete.Infrastructure.Data.Contexts;
using UtterlyComplete.Infrastructure.Data.Repositories;

namespace UtterlyComplete.Infrastructure.Data
{
    public static class ServiceRegistration
    {
        public static void AddDataAbstractionLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlite(
                    configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                ));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));
        }
    }
}
