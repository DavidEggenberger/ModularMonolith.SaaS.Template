using Shared.Features.DomainKernel;

namespace Shared.Features.CQRS.DomainEvent
{
    public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
    {
        Task HandleAsync(TDomainEvent query, CancellationToken cancellation);
    }
}
