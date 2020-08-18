using Recruiting.BL.Services;
using Recruiting.BL.Services.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<IApplicationService, ApplicationService>();

            return services;
        }
    }
}
