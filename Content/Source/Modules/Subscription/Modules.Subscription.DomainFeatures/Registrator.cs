using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Subscription.DomainFeatures.Infrastructure;
using Stripe;

namespace Modules.Subscription.DomainFeatures
{
    public static class Registrator
    {
        public static IServiceCollection RegisterSubscriptionModule(this IServiceCollection services, IConfiguration configuration, IHostEnvironment webHostEnvironment)
        {
            StripeConfiguration.ApiKey = configuration[StripeConfigurationConstants.StripeAPIKey];
            services.Configure<StripeOptions>(stripeOptions =>
            {
                stripeOptions.ProfessionalPlanPriceId = configuration[StripeConfigurationConstants.StripeProfessionalPlanId];
                stripeOptions.EnterprisePlanPriceId = configuration[StripeConfigurationConstants.StripeEnterprisePlanId];
            });



            return services;
        }
    }
}
