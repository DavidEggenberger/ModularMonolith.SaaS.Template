using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.DomainKernel.BuildingBlocks.ExecutionContextAccess;
using Shared.Kernel.BuildingBlocks;

namespace Web.Server.BuildingBlocks.ExecutionContextAccess
{
    public static class Registrator
    {
        public static IServiceCollection RegisterExecutionContextAccessor(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            //services.AddScoped<IExecutionContextAccessor>(provider =>
            //{
            //    return new ExecutionContextAccessor(provider.GetRequiredService<IHttpContextAccessor>().HttpContext);
            //});
            services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();
            return services;
        }
    }
}
