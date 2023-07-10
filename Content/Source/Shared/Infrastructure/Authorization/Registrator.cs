using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Authorization
{
    public static class Registrator
    {
        public static IServiceCollection RegisterAuthorization(this IServiceCollection services)
        {
            return services.AddScoped<IAuthorizationService, AuthorizationService>();
        }
    }
}
