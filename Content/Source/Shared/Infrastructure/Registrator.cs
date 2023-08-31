using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.CQRS;
using Shared.Infrastructure.EFCore;
using Shared.Infrastructure.EmailSender;
using Shared.Infrastructure.Modules;
using Shared.Infrastructure.MultiTenancy;

namespace Shared.Infrastructure
{
    public static class Registrator
    {
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var startupModules = serviceProvider.GetRequiredService<IEnumerable<IModuleStartup>>();

            services.RegisterCQRS(startupModules.Select(x => x.GetType().Assembly).ToArray());
            services.RegisterEFCore(configuration);
            services.RegisterEmailSender(configuration);
            services.RegisterMultiTenancy();

            return services;
        }
    }
}
