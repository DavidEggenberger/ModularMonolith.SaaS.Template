using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Authorization;
using Shared.Infrastructure.CQRS;
using Shared.Infrastructure.EFCore;
using Shared.Infrastructure.EmailSender;
using Shared.Infrastructure.Modules;
using Shared.Infrastructure.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure
{
    public static class Registrator
    {
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var startupModules = serviceProvider.GetRequiredService<IEnumerable<IModuleStartup>>();
            
            services.RegisterAuthorization();
            services.RegisterCQRS(startupModules.Select(x => x.GetType().Assembly).ToArray());
            services.RegisterEFCore(configuration);
            services.RegisterEmailSender(configuration);
            services.RegisterMultiTenancy();

            return services;
        }
    }
}
