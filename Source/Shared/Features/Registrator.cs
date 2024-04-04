using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Features.Messaging;
using Shared.Features.EFCore;
using Shared.Features.EmailSender;
using Shared.Features.Modules;
using Shared.Features.Server.ExecutionContext;

namespace Shared.Features
{
    public static class Registrator
    {
        public static IServiceCollection AddSharedFeatures(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var startupModules = serviceProvider.GetRequiredService<IEnumerable<IModule>>();

            services.AddMessaging(startupModules.Where(sm => sm.FeaturesAssembly is not null).Select(x => x.FeaturesAssembly).ToArray());
            services.AddEFCore(configuration);
            services.AddEmailSender(configuration);

            services.AddServerExecutionContext();

            return services;
        }

        public static IApplicationBuilder UseSharedFeaturesMiddleware(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEFCoreMiddleware();
            app.UseServerExecutionContextMiddleware();
            app.UseModulesMiddleware(env);

            return app;
        }
    }
}
