using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.DomainFeatures.EFCore.Configuration;

namespace Shared.DomainFeatures.EFCore
{
    public static class Registrator
    {
        public static IServiceCollection RegisterEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EFCoreConfiguration>(configuration.GetSection(nameof(EFCoreConfiguration)));
            services.AddScoped<EFCoreConfiguration>(sp => sp.GetRequiredService<IOptions<EFCoreConfiguration>>().Value);
            services.AddSingleton<IValidateOptions<EFCoreConfiguration>, EFCoreConfigurationValidator>();

            return services;
        }
    }
}
