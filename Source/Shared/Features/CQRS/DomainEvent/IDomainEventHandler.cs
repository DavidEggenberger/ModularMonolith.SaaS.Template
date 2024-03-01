using Shared.Features.Domain;
using Shared.Features.Server;

namespace Shared.Features.CQRS.DomainEvent
{
    public interface IDomainEventHandler<in TDomainEvent> : IInServerExecutionScope where TDomainEvent : IDomainEvent
    {
        Task HandleAsync(TDomainEvent query, CancellationToken cancellation);
    }
}
