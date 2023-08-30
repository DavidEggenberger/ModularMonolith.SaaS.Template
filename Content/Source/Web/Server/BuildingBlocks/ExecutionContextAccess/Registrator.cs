using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks;

namespace Web.Server.BuildingBlocks.ExecutionContextAccess
{
    public static class Registrator
    {
        public static IServiceCollection RegisterExecutionContextAccessor(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ExecutionContextAccessor>();
            services.AddScoped<IExecutionContextAccessor, ExecutionContextAccessor>();
            return services;
        }

        public static IApplicationBuilder RegisterExecutionContextAccessingMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Use(async (context, next) =>
            {
                var executionContextAccessor = context.RequestServices.GetService<ExecutionContextAccessor>();
                executionContextAccessor.CaptureHttpContext(context);
                await next(context);
            });

            return applicationBuilder;
        }
    }
}
