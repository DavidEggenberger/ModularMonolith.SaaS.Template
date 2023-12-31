using Microsoft.Extensions.DependencyInjection;
using Shared.Features.MultiTenancy.Services;

namespace Shared.Features.MultiTenancy
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
