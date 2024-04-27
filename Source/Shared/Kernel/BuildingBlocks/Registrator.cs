using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks.Auth;
using Shared.Kernel.BuildingBlocks.Services.ModelValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Kernel.BuildingBlocks
{
    public static class Registrator
    {
        public static IServiceCollection AddSharedKernel(this IServiceCollection services)
        {
            services.AddAuth();
            services.AddModelValidation();


            return services;
        }
    }
}
