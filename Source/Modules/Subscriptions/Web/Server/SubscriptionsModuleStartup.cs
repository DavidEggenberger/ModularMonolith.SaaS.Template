using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Subscriptions.Features.Infrastructure.Configuration;
using Modules.Subscriptions.Features.Infrastructure.EFCore;
using Shared.Features.EFCore;
using Shared.Features.Misc.Configuration;
using Shared.Features.Misc.Modules;
using Stripe;

namespace Modules.Subscriptions.Web.Server
{
    public class SubscriptionsModuleStartup : IModuleStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.RegisterDbContext<SubscriptionsDbContext>();
            services.RegisterConfiguration<SubscriptionsConfiguration, SubscriptionsConfigurationValidator>(config);

            StripeConfiguration.ApiKey = services.BuildServiceProvider().GetRequiredService<SubscriptionsConfiguration>().StripeAPIKey;
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }
    }
}
