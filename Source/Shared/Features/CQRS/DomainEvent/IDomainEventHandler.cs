using Shared.Features.Domain;
using Shared.Features.Server.ExecutionContext;

namespace Shared.Features.CQRS.DomainEvent
{
    public interface IDomainEventHandler<in TDomainEvent> : IInServerExecutionContextScope where TDomainEvent : IDomainEvent
    {
        Task HandleAsync(TDomainEvent query, CancellationToken cancellation);
    }
}
