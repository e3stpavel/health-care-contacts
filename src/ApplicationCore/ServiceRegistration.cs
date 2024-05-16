using Microsoft.Extensions.DependencyInjection;
using UtterlyComplete.ApplicationCore.Interfaces.Services;
using UtterlyComplete.ApplicationCore.Mappings;
using UtterlyComplete.ApplicationCore.Models;
using UtterlyComplete.ApplicationCore.Services;

namespace UtterlyComplete.ApplicationCore
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PartyProfile).Assembly);

            return services;
        }

        public static IServiceCollection AddApplicationCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IService<PartyDto>, PartyService>();
            services.AddScoped<IService<FacilityDto>, FacilityService>();

            return services;
        }
    }
}
