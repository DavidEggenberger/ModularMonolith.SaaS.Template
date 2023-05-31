using Microsoft.Extensions.DependencyInjection;
using Shared.DomainFeatures.Authorization;
using Shared.DomainFeatures.BuildingBlocks.ExecutionAccessor;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.Authorization.Services;

namespace Shared.DomainFeatures
{
    public static class DomainFeaturesDIRegistrator
    {
        /// <summary>
        /// Registers the <see cref="UserAuthorizationService"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDomainFeatures(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();
            services.AddScoped<IUserAuthorizationService, UserAuthorizationService>();

            return services;
        }
    }
}
