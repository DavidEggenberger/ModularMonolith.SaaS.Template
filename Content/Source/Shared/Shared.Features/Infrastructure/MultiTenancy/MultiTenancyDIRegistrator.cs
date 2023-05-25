using Microsoft.Extensions.DependencyInjection;
using Shared.Features.Infrastructure.MultiTenancy.Services;

namespace Shared.Features.Infrastructure.MultiTenancy
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
