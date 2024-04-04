using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.Auth.Attributes;

namespace Shared.Features.Messaging.Query
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task<TQueryResult> DispatchAsync<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation = default) where TQuery : IQuery<TQueryResult>
        {
            var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();
            var executionContext = serviceProvider.GetRequiredService<IExecutionContext>();

            var authorizationAttribute = Attribute.GetCustomAttributes(typeof(TQuery)).First(a => a is AuthorizationAttribute) as AuthorizationAttribute;
            

            return handler.HandleAsync(query, cancellation);
        }
    }
}
