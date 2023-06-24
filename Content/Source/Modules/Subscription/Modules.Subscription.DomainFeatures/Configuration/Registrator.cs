using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Modules.Subscription.DomainFeatures.Configuration
{
    public static class Registrator
    {
        public static IServiceCollection RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SubscriptionConfiguration>(configuration.GetSection(nameof(SubscriptionConfiguration)));
            services.AddSingleton<IValidateOptions<SubscriptionConfiguration>, SubscriptionConfigurationValidator>();

            return services;
        }
    }
}
