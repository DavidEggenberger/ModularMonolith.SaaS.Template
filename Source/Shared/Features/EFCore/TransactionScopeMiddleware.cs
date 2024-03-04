using Microsoft.AspNetCore.Http;
using System.Transactions;

namespace Shared.Features.EFCore
{
    public class TransactionScopeMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await next(context);

                scope.Complete();
            }
        }
    }
}