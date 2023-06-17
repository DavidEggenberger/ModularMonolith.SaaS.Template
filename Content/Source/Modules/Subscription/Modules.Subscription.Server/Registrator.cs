using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Subscription.DomainFeatures.Infrastructure;
using Stripe;

namespace Modules.Subscription.Server
{
    public static class Registrator
    {
        public static IMvcBuilder RegisterSubscriptionModuleControllers(this IMvcBuilder mvcBuilder, IConfiguration configuration)
        {
            mvcBuilder.AddApplicationPart(typeof(Registrator).Assembly);            
            StripeConfiguration.ApiKey = configuration["Stripe:StripeKey"];
            mvcBuilder.Services.Configure<StripeOptions>(stripeOptions =>
            {
                stripeOptions.ProfessionalPlanPriceId = configuration["Stripe:StripeKey"];
                stripeOptions.EnterprisePlanPriceId = configuration["Stripe:StripeKey"];
            });

            return mvcBuilder;
        }
    }
}
