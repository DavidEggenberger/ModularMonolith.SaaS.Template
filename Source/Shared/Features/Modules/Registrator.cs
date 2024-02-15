using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.CQRS;

namespace Shared.Features.Modules
{
    public static class Registrator
    {
        public static IServiceCollection AddModule<TStartup>(this IServiceCollection services, IConfiguration config = null)
            where TStartup : IModuleStartup, new()
        {
            // Register assembly in MVC so it can find controllers of the module
            services.AddControllers().ConfigureApplicationPartManager(manager =>
                manager.ApplicationParts.Add(new AssemblyPart(typeof(TStartup).Assembly)));

            var startup = new TStartup();
            startup.ConfigureServices(services, config);

            services.AddSingleton(new Module(startup));

            return services;
        }

        public static void AddModules(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var startupModules = serviceProvider.GetRequiredService<IEnumerable<Module>>();

            services.RegisterCQRS(startupModules.Where(sm => sm.Startup.FeaturesAssembly is not null).Select(sm => sm.Startup.FeaturesAssembly).ToArray());
        }

        public static IApplicationBuilder UseModules(this IApplicationBuilder app, IHostEnvironment env)
        {
            // Adds endpoints defined in modules
            var modules = app
                .ApplicationServices
                .GetRequiredService<IEnumerable<Module>>();
            foreach (var module in modules)
            {
                module.Startup.Configure(app, env);
            }

            return app;
        }      
    }
}
