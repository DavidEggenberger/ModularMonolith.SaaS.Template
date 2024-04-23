using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Subscriptions.Features.Infrastructure.Configuration;
using Modules.Subscriptions.Features.Infrastructure.EFCore;
using Shared.Features.EFCore;
using Shared.Features.Modules;
using Shared.Features.Modules.Configuration;
using Stripe;

namespace Modules.Subscriptions.Server
{
    public class SubscriptionsModuleStartup : IModuleStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.RegisterDbContext<SubscriptionsDbContext>();
            services.RegisterModuleConfiguration<SubscriptionsConfiguration, SubscriptionsConfigurationValidator>(config);

            StripeConfiguration.ApiKey = services.BuildServiceProvider().GetRequiredService<SubscriptionsConfiguration>().StripeAPIKey;
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }
    }
}
