using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.Configuration
{
    public static class Registrator
    {
        public static IServiceCollection RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TenantIdentityConfiguration>(configuration.GetSection(nameof(TenantIdentityConfiguration)));
            services.AddSingleton<IValidateOptions<TenantIdentityConfiguration>, TenantIdentityConfigurationValidator>();

            return services;
        }
    }
}
