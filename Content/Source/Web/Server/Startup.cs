using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.LandingPages.Web.Server;
using Modules.TenantIdentity.DomainFeatures;
using Modules.TenantIdentity.Web.Server;
using Modules.Subscription.Server;
using Modules.Subscription.DomainFeatures;
using Shared.Infrastructure;
using System.Reflection;
using Web.Server.BuildingBlocks;
using Shared.Infrastructure.Modules;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

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

            services.AddBuildingBlocks();
            services.RegisterSharedInfrastructure();

            services.AddModule<TenantIdentityStartup>();
            services.AddModule<SubscriptionStartup>();
            services.AddModule<LandingPagesStartup>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.RegisterBuildingBlocks();

            app.UseModules(env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
