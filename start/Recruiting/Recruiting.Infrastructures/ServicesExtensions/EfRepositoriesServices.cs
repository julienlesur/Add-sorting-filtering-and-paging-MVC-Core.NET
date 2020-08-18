using Recruiting.Data.EfRepositories;
using Recruiting.Data.EfRepositories.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EfRepositoriesServices
    {
        public static IServiceCollection AddEfRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEfJobRepository, EfJobRepository>();
            services.AddScoped<IEfApplicantRepository, EfApplicantRepository>();
            services.AddScoped<IEfApplicationRepository, EfApplicationRepository>();
            services.AddScoped<IEfUnitRepository, EfUnitRepository>();

            return services;
        }

    }
}
