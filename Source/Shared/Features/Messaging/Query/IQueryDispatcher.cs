namespace Shared.Features.Messaging.Query
{
    public interface IQueryDispatcher
    {
        Task<TQueryResult> DispatchAsync<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation = default) where TQuery : IQuery<TQueryResult>;
    }
}
