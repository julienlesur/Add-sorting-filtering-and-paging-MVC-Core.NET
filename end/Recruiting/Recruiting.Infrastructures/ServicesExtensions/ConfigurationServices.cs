using Microsoft.Extensions.Configuration;
using Recruiting.Infrastructures.Configurations;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<GridConfiguration>(c => config.GetSection(GridConfiguration.GridOptions));

            return services;
        }
    }
}
