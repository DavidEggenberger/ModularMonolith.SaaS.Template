using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Infrastructure.EFCore;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure.Configuration
{
    public static class Registrator
    {
        public static IServiceCollection RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TenantIdentityConfiguration>(configuration.GetSection(nameof(TenantIdentityConfiguration)));
            services.AddScoped<TenantIdentityConfiguration>(sp => sp.GetRequiredService<IOptions<TenantIdentityConfiguration>>().Value);
            services.AddSingleton<IValidateOptions<TenantIdentityConfiguration>, TenantIdentityConfigurationValidator>();

            return services;
        }
    }
}
