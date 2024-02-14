using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks;
using System.Transactions;

namespace Shared.Features.Server.ExecutionContext
{
    public static class Registrator
    {
        public static IServiceCollection AddServerExecutionContext(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ServiceExecutionContextMiddleware>();
            services.AddScoped<IExecutionContext, ServerExecutionContext>(ServerExecutionContext.CreateInstance);
            return services;
        }

        public static IApplicationBuilder UseServerExecutionContextMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ServiceExecutionContextMiddleware>();

            return app;
        }
    }
    

    public class ServiceExecutionContextMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var executionContext = (ServerExecutionContext)context.RequestServices.GetService<IExecutionContext>();
            executionContext.InitializeInstance(context);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await next(context);

                scope.Complete();
            }
        }
    }
}
