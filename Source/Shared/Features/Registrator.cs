using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Features.CQRS;
using Shared.Features.EFCore;
using Shared.Features.EmailSender;
using Shared.Features.Modules;

namespace Shared.Features
{
    public static class Registrator
    {
        public static IServiceCollection AddSharedFeatures(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var startupModules = serviceProvider.GetRequiredService<IEnumerable<Module>>();

            services.RegisterCQRS(startupModules.Where(sm => sm.Startup.FeaturesAssembly is not null).Select(x => x.Startup.FeaturesAssembly).ToArray());
            services.RegisterEFCore(configuration);
            services.RegisterEmailSender(configuration);

            return services;
        }

        public static IApplicationBuilder UseSharedFeaturesMiddleware(this IApplicationBuilder app)
        {
            app.UseEFCoreMiddleware();

            return app;
        }
    }
}
