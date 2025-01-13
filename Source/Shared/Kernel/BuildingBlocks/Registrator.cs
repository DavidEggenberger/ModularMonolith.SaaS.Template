using Microsoft.Extensions.DependencyInjection;

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
