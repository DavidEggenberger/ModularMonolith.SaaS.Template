using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Subscription.Features.Infrastructure.Configuration;
using Modules.Subscription.Features.Infrastructure.EFCore;
using Shared.Features.EFCore;
using Shared.Features.Modules;
using System.Reflection;

namespace Modules.Subscription.Server
{
    public class SubscriptionsModuleStartup : IModuleStartup
    {
        public Assembly? FeaturesAssembly => typeof(SubscriptionsModuleStartup).Assembly;

        public void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.RegisterDbContext<SubscriptionsDbContext>();
            services.RegisterConfiguration(config);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }
    }
}
