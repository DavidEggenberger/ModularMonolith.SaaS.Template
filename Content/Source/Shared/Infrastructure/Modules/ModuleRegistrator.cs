using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Shared.Infrastructure.Modules
{
    public static class ModuleRegistrator
    {
        public static IServiceCollection AddModule<TStartup>(this IServiceCollection services)
            where TStartup : IModuleStartup, new()
        {
            // Register assembly in MVC so it can find controllers of the module
            services.AddControllers().ConfigureApplicationPartManager(manager =>
                manager.ApplicationParts.Add(new AssemblyPart(typeof(TStartup).Assembly)));

            var startup = new TStartup();
            startup.ConfigureServices(services);

            services.AddSingleton(new Module(startup));

            return services;
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
