using Microsoft.Extensions.DependencyInjection;
using Shared.DomainFeatures.MultiTenancy.Services;

namespace Shared.DomainFeatures.MultiTenancy
{
    public static class MultiTenancyDIRegistrator
    {
        public static IServiceCollection RegisterMultiTenancy(this IServiceCollection services)
        {
            services.AddScoped<ITenantResolver, TenantResolver>();

            return services;
        }
    }
}
