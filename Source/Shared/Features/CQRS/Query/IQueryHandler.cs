using Shared.Features.Server.ExecutionContext;

namespace Shared.Features.CQRS.Query
{
    public interface IQueryHandler<in TQuery, TQueryResult> : IInServerExecutionContextScope where TQuery : IQuery<TQueryResult>
    {
        Task<TQueryResult> HandleAsync(TQuery query, CancellationToken cancellation);
    }
}
