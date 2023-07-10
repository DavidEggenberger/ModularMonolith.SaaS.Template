using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Authorization;
using Shared.Infrastructure.CQRS;
using Shared.Infrastructure.EFCore;
using Shared.Infrastructure.EmailSender;
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
        public static IServiceCollection RegisterSharedInfrastructure(this IServiceCollection services, Assembly[] assemblies)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            services.RegisterAuthorization();
            services.RegisterCQRS(assemblies);
            services.RegisterEFCore(configuration);
            services.RegisterEmailSender(configuration);
            services.RegisterMultiTenancy();

            return services;
        }
    }
}
