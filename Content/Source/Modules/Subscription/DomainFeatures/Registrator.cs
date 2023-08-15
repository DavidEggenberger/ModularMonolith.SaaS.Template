using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Subscription.DomainFeatures.Infrastructure;
using Modules.Subscription.DomainFeatures.Infrastructure.Configuration;
using Modules.Subscription.DomainFeatures.Infrastructure.EFCore;
using Stripe;

namespace Modules.Subscription.DomainFeatures
{
    public static class Registrator
    {
        public static IServiceCollection RegisterSubscriptionModule(this IServiceCollection services)
        {
            //services.RegisterConfiguration(configuration);
            services.AddDbContext<SubscriptionDbContext>();


            return services;
        }
    }
}
