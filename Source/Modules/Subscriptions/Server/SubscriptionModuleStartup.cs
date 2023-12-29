using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Subscription.Features;
using Modules.Subscription.Features.Infrastructure.Configuration;
using Modules.Subscription.Features.Infrastructure.EFCore;
using Shared.Features.Modules;
using System.Reflection;

namespace Modules.Subscription.Server
{
    public class SubscriptionModuleStartup : IModuleStartup
    {
        public Assembly? FeaturesAssembly => typeof(IAssemblyMarker).Assembly;

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
