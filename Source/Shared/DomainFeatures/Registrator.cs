using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.DomainFeatures.CQRS;
using Shared.DomainFeatures.EFCore;
using Shared.DomainFeatures.EmailSender;
using Shared.DomainFeatures.Modules;
using Shared.DomainFeatures.MultiTenancy;

namespace Shared.DomainFeatures
{
    public static class Registrator
    {
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var startupModules = serviceProvider.GetRequiredService<IEnumerable<Module>>();

            services.RegisterCQRS(startupModules.Where(sm => sm.Startup.DomainFeaturesAssembly is not null).Select(x => x.Startup.DomainFeaturesAssembly).ToArray());
            services.RegisterEFCore(configuration);
            services.RegisterEmailSender(configuration);
            services.RegisterMultiTenancy();

            return services;
        }
    }
}
