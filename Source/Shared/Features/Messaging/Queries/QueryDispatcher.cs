using Microsoft.Extensions.DependencyInjection;
using Shared.Features.Server.ExecutionContext;

namespace Shared.Features.Messaging.Queries
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task<TQueryResult> DispatchAsync<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation = default) where TQuery : Query<TQueryResult>
        {
            var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();
            var executionContext = serviceProvider.GetRequiredService<IExecutionContext>();

            return handler.HandleAsync(query, cancellation);
        }
    }
}
