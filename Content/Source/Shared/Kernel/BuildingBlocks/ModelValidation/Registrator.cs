using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Kernel.BuildingBlocks.ModelValidation
{
    public static class Registrator
    {
        public static IServiceCollection RegisterModelValidationService(this IServiceCollection services)
        {
            return services.AddScoped<IValidationService, ValidationService>();
        }
    }
}
