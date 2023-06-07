using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.DomainFeatures.Authorization;
using Shared.DomainFeatures.BuildingBlocks.ExecutionAccessor;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.Authorization.Services;

namespace Shared.DomainFeatures
{
    public static class Registrator
    {
        /// <summary>
        /// Registers the <see cref="UserAuthorizationService"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterSharedDomainFeaturesServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IExecutionContextAccessor>(provider =>
            {
                return new ExecutionContextAccessor(provider.GetRequiredService<IHttpContextAccessor>().HttpContext);
            });
            services.AddScoped<IUserAuthorizationService, UserAuthorizationService>();

            return services;
        }
    }
}
