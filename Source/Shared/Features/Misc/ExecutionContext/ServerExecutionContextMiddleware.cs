using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Features.Misc.ExecutionContext
{
    public class ServerExecutionContextMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var executionContext = (ServerExecutionContext)context.RequestServices.GetService<IExecutionContext>();
            executionContext.InitializeInstance(context);

            await next(context);
        }
    }
}
