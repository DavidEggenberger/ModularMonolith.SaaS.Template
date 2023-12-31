using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Stripe;

namespace Modules.Subscription.Features.Infrastructure.Configuration
{
    public static class Registrator
    {
        public static IServiceCollection RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["SubscriptionConfiguration:StripeAPIKey"];

            services.Configure<SubscriptionConfiguration>(configuration.GetSection(nameof(SubscriptionConfiguration)));
            services.AddScoped<SubscriptionConfiguration>(sp =>
            {
                SubscriptionConfiguration sc = new SubscriptionConfiguration();
                sp.GetRequiredService<IConfiguration>().GetSection("SubscriptionConfiguration").Bind(sc);
                return sc;
            });
            services.AddSingleton<IValidateOptions<SubscriptionConfiguration>, SubscriptionConfigurationValidator>();

            return services;
        }
    }
}
