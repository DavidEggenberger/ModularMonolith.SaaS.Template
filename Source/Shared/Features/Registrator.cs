using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Features.CQRS;
using Shared.Features.EFCore;
using Shared.Features.EmailSender;
using Shared.Features.Modules;
using Shared.Features.MultiTenancy;

namespace Shared.Features
{
    public static class Registrator
    {
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var startupModules = serviceProvider.GetRequiredService<IEnumerable<Module>>();

            services.RegisterCQRS(startupModules.Where(sm => sm.Startup.FeaturesAssembly is not null).Select(x => x.Startup.FeaturesAssembly).ToArray());
            services.RegisterEFCore(configuration);
            services.RegisterEmailSender(configuration);
            services.RegisterMultiTenancy();

            return services;
        }
    }
}
