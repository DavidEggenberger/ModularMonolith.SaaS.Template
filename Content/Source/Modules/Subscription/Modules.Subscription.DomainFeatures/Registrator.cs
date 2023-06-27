using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Subscription.DomainFeatures.Infrastructure;
using Modules.Subscription.DomainFeatures.Infrastructure.Configuration;
using Stripe;

namespace Modules.Subscription.DomainFeatures
{
    public static class Registrator
    {
        public static IServiceCollection RegisterSubscriptionModule(this IServiceCollection services, IConfiguration configuration, IHostEnvironment webHostEnvironment)
        {
            services.RegisterConfiguration(configuration);

            return services;
        }
    }
}
