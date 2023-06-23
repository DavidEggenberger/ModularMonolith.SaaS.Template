using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Subscription.DomainFeatures.Configuration;
using Modules.Subscription.DomainFeatures.Infrastructure;
using Stripe;

namespace Modules.Subscription.DomainFeatures
{
    public static class Registrator
    {
        public static IServiceCollection RegisterSubscriptionModule(this IServiceCollection services, IConfiguration configuration, IHostEnvironment webHostEnvironment)
        {
            services.RegisterConfiguration(configuration);

            Stripe.StripeConfiguration.ApiKey = configuration[StripeConfiguration.StripeAPIKey];
            services.Configure<StripeOptions>(stripeOptions =>
            {
                stripeOptions.ProfessionalPlanPriceId = configuration[StripeConfiguration.StripeProfessionalPlanId];
                stripeOptions.EnterprisePlanPriceId = configuration[StripeConfiguration.StripeEnterprisePlanId];
            });



            return services;
        }
    }
}
