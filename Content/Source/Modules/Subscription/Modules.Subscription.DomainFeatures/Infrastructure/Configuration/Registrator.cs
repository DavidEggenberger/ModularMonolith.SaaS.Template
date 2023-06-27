using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Modules.Subscription.DomainFeatures.Infrastructure;

namespace Modules.Subscription.DomainFeatures.Infrastructure.Configuration
{
    public static class Registrator
    {
        public static IServiceCollection RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            Stripe.StripeConfiguration.ApiKey = configuration[SubscriptionConfiguration.StripeAPIKeyConstant];

            services.Configure<SubscriptionConfiguration>(configuration.GetSection(nameof(SubscriptionConfiguration)));
            services.AddSingleton<IValidateOptions<SubscriptionConfiguration>, SubscriptionConfigurationValidator>();

            return services;
        }
    }
}
