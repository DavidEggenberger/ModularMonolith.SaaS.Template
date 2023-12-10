using Microsoft.AspNetCore.Builder;

namespace Shared.DomainFeatures.MultiTenancy
{
    public static class MultiTenancyMdwRegistrator
    {
        public static IApplicationBuilder UseMultiTenancyMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<MultiTenancySecurityMiddleware>();
        }
    }
}
