using Microsoft.Extensions.DependencyInjection;
using UtterlyComplete.ApplicationCore.Mappings;

namespace UtterlyComplete.ApplicationCore
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PartyProfile).Assembly);

            return services;
        }
    }
}
