using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks;

namespace Web.Server.BuildingBlocks.ServerExecutionContext
{
    public static class Registrator
    {
        public static IServiceCollection RegisterExecutionContext(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IExecutionContext, ServerExecutionContext>(ServerExecutionContext.CreateInstance);
            return services;
        }

        public static IApplicationBuilder RegisterExecutionContextMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Use(async (context, next) =>
            {
                var executionContext = (ServerExecutionContext)context.RequestServices.GetService<IExecutionContext>();
                executionContext.InitializeInstance(context);

                await next(context);

                await executionContext.CommitChangesAsync();
            });

            return applicationBuilder;
        }
    }
}
