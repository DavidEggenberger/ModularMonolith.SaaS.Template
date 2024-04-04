using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.Messaging;

namespace Shared.Features.Modules
{
    public static class Registrator
    {
        public static IServiceCollection AddModule<TModuleStartup>(this IServiceCollection services, IConfiguration config = null)
            where TModuleStartup : IModuleStartup
        {
            // Register assembly in MVC so it can find controllers of the module
            services.AddControllers().ConfigureApplicationPartManager(manager =>
                manager.ApplicationParts.Add(new AssemblyPart(typeof(TModuleStartup).Assembly)));

            var moduleStartup = (IModuleStartup)Activator.CreateInstance(typeof(TModuleStartup));
            moduleStartup.ConfigureServices(services, config);

            return services;
        }

        public static IServiceCollection AddModule<TModule, TModuleStartup>(this IServiceCollection services, IConfiguration config = null)
            where TModule : IModule 
            where TModuleStartup : IModuleStartup
        {
            // Register assembly in MVC so it can find controllers of the module
            services.AddControllers().ConfigureApplicationPartManager(manager =>
                manager.ApplicationParts.Add(new AssemblyPart(typeof(TModuleStartup).Assembly)));

            var moduleStartup = (IModuleStartup)Activator.CreateInstance(typeof(TModuleStartup));
            moduleStartup.ConfigureServices(services, config);

            var module = (IModule)ActivatorUtilities.CreateInstance<TModule>(services.BuildServiceProvider());
            services.AddScoped<IModule>(sp => module);

            return services;
        }

        public static void AddModules(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var startupModules = serviceProvider.GetRequiredService<IEnumerable<IModule>>();

            services.AddMessaging(startupModules.Where(sm => sm.FeaturesAssembly is not null).Select(sm => sm.FeaturesAssembly).ToArray());
        }

        public static IApplicationBuilder UseModulesMiddleware(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                foreach (var module in context.RequestServices.GetRequiredService<IEnumerable<IModuleStartup>>())
                {
                    module.Configure(app, env);
                }

                await next();
            });

            return app;
        }    
    }
}
