namespace Shared.Features.Messaging.Queries
{
    public interface IQueryHandler<in TQuery, TQueryResult> where TQuery : Query<TQueryResult>
    {
        Task<TQueryResult> HandleAsync(TQuery query, CancellationToken cancellation);
    }
}
