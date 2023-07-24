using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Subscription.DomainFeatures.Infrastructure;
using Stripe;

namespace Modules.Subscription.Server
{
    public static class Registrator
    {
        public static IMvcBuilder RegisterSubscriptionModuleControllers(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddApplicationPart(typeof(Registrator).Assembly);            

            return mvcBuilder;
        }
    }
}
