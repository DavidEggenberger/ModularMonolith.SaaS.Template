using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Infrastructure.EFCore.Configuration;

namespace Shared.Infrastructure.EFCore
{
    public static class EFCoreDIRegistrator
    {
        public static IServiceCollection RegisterEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EFCoreConfiguration>(configuration.GetSection(nameof(EFCoreConfiguration)));
            services.AddSingleton<IValidateOptions<EFCoreConfiguration>, EFCoreConfigurationValidator>();

            return services;
        }
    }
}
