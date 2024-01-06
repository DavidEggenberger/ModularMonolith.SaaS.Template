using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Modules.TenantIdentity.Features.Infrastructure.Configuration
{
    public static class Registrator
    {
        public static IServiceCollection RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TenantIdentityConfiguration>(configuration.GetSection(nameof(TenantIdentityConfiguration)));
            services.AddScoped<TenantIdentityConfiguration>(sp => 
            {
                TenantIdentityConfiguration tc = new TenantIdentityConfiguration();
                sp.GetRequiredService<IConfiguration>().GetSection("TenantIdentityConfiguration").Bind(tc);
                return tc;
            });
            services.AddSingleton<IValidateOptions<TenantIdentityConfiguration>, TenantIdentityConfigurationValidator>();

            return services;
        }
    }
}
