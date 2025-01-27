﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.Messaging;

namespace Shared.Features.Misc.Modules
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
            where TModule : class, IModule
            where TModuleStartup : IModuleStartup
        {
            // Register assembly in MVC so it can find controllers of the module
            services.AddControllers().ConfigureApplicationPartManager(manager =>
                manager.ApplicationParts.Add(new AssemblyPart(typeof(TModuleStartup).Assembly)));

            var moduleStartup = (IModuleStartup)Activator.CreateInstance(typeof(TModuleStartup));
            moduleStartup.ConfigureServices(services, config);

            services.AddScoped<TModule>();
            services.AddMessagingForModule(typeof(TModule));

            return services;
        }

        public static IApplicationBuilder UseModulesMiddleware(this IApplicationBuilder app, IHostEnvironment env)
        {
            using var scope = app.ApplicationServices.CreateAsyncScope();

            foreach (var moduleStartup in scope.ServiceProvider.GetRequiredService<IEnumerable<IModuleStartup>>())
            {
                moduleStartup.Configure(app, env);
            }

            return app;
        }
    }
}
