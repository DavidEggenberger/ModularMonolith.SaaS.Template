using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.DomainFeatures.Authorization;
using Shared.DomainFeatures.BuildingBlocks.ExecutionAccessor;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.Authorization.Services;

namespace Shared.DomainFeatures
{
    public static class DomainFeaturesMdwRegistrator
    {
        /// <summary>
        /// Registers the <see cref="ExecutionAccessingMiddleware"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IApplicationBuilder RegisterDomainFeatures(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExecutionAccessingMiddleware>();

            return applicationBuilder;
        }
    }
}
