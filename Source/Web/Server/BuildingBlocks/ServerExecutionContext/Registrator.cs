using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks.ExecutionContext;

namespace Web.Server.BuildingBlocks.ServerExecutionContext
{
    public static class Registrator
    {
        public static IServiceCollection RegisterExecutionContext(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IExecutionContext, ExecutionContext>(ExecutionContext.GetInstance);
            return services;
        }

        public static IApplicationBuilder RegisterExecutionContextMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Use(async (context, next) =>
            {
                var executionContext = (ExecutionContext)context.RequestServices.GetService<IExecutionContext>();
                executionContext.CaptureHttpContext(context);

                await next(context);
            });

            return applicationBuilder;
        }
    }
}
