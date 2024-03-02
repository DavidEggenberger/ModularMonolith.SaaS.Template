using Shared.Features.Server;

namespace Shared.Features.CQRS.Query
{
    public interface IQueryHandler<in TQuery, TQueryResult> : ServerExecutionBase where TQuery : IQuery<TQueryResult>
    {
        Task<TQueryResult> HandleAsync(TQuery query, CancellationToken cancellation);
    }
}
