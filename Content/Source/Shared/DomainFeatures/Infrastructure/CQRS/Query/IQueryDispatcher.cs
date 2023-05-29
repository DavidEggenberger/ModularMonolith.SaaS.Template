namespace Shared.DomainFeatures.Infrastructure.CQRS.Query
{
    public interface IQueryDispatcher
    {
        Task<TQueryResult> DispatchAsync<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation = default) where TQuery : IQuery<TQueryResult>;
    }
}
