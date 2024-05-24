using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks.Auth;

namespace Shared.Kernel.BuildingBlocks
{
    public static class Registrator
    {
        public static IServiceCollection AddSharedKernel(this IServiceCollection services)
        {
            services.AddAuth();


            return services;
        }
    }
}
