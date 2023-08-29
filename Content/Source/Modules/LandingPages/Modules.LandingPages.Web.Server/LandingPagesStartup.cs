using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Infrastructure.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.LandingPages.Web.Server
{
    public class LandingPagesStartup : IModuleStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddApplicationPart(typeof(LandingPagesStartup).Assembly);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapFallbackToPage("/LandingPage");
            });
        }
    }
}
