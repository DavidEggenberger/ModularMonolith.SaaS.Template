using Microsoft.Extensions.DependencyInjection;

namespace Shared.Features.CQRS.Query
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
            return handler.HandleAsync(query, cancellation);
        }
    }
}
