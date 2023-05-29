using Microsoft.Extensions.DependencyInjection;
using Shared.DomainFeatures.Infrastructure.MultiTenancy.Services;

namespace Shared.DomainFeatures.Infrastructure.MultiTenancy
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
