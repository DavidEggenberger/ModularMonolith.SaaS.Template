using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Subscription.DomainFeatures.Infrastructure.Configuration;
using Modules.Subscription.DomainFeatures.Infrastructure.EFCore;
using Shared.Infrastructure.Modules;

namespace Modules.Subscription.Server
{
    public class SubscriptionModuleStartup : IModuleStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration config = null)
        {
            services.AddDbContext<SubscriptionDbContext>();
            services.RegisterConfiguration(services.BuildServiceProvider().GetRequiredService<IConfiguration>());
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }
    }
}
