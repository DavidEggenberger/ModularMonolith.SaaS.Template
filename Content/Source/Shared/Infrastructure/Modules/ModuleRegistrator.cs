using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
