using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.LandingPages.Web.Server;
using Web.Server.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Modules.Subscription.Server;
using Modules.TenantIdentity.Server;
using Shared.Features.Modules;
using Shared.Features;

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
            Action<MvcOptions> configureControllerOptions = options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            };

            Action<JsonOptions> configureJsonOptions = options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
            };

            services.AddControllers();
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddAuth();

            services.AddBuildingBlocks();
            services.AddSharedInfrastructure();

            services.AddModule<TenantIdentityModuleStartup>(Configuration);
            services.AddModule<SubscriptionModuleStartup>();
            services.AddModule<LandingPagesModuleStartup>();
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
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBuildingBlocks();

            app.UseModules(env);

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
