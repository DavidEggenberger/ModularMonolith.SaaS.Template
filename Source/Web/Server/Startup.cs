using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.LandingPages.Web.Server;
using Modules.Subscriptions.Features;
using Modules.Subscriptions.Server;
using Modules.TenantIdentity.Features;
using Modules.TenantIdentity.Server;
using Shared.Features;
using Shared.Features.Modules;
using Shared.Kernel.BuildingBlocks;
using Web.Server.BuildingBlocks;

namespace Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSharedKernel();
            services.AddSharedFeatures();
            services.AddBuildingBlocks();

            services.AddModule<TenantIdentityModule, TenantIdentityModuleStartup>(Configuration);
            services.AddModule<SubscriptionsModule, SubscriptionsModuleStartup>(Configuration);
            services.AddModule<LandingPagesModuleStartup>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSharedFeaturesMiddleware(env);
            app.UseBuildingBlocksMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();

                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
