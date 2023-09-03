using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Infrastructure.Modules;

namespace Modules.LandingPages.Web.Server
{
    public class LandingPagesModuleStartup : IModuleStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddApplicationPart(typeof(LandingPagesModuleStartup).Assembly);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }
    }
}
