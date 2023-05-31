using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.DomainFeatures.BuildingBlocks.ExecutionAccessor
{
    public class ExecutionAccessingMiddleware : IMiddleware
    {
        private readonly IServiceCollection serviceCollection;

        public ExecutionAccessingMiddleware(IServiceCollection serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            serviceCollection.AddScoped<ExecutionContextAccessor>(serviceProvider => new ExecutionContextAccessor(context));

            return next(context);
        }
    }
}
