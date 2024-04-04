namespace Shared.Features.Messaging.Query
{
    public interface IQueryHandler<in TQuery, TQueryResult> where TQuery : IQuery<TQueryResult>
    {
        Task<TQueryResult> HandleAsync(TQuery query, CancellationToken cancellation);
    }
}
