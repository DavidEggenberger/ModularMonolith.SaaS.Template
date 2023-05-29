using Shared.DomainFeatures.DomainKernel.Events;

namespace Shared.DomainFeatures.Infrastructure.CQRS.DomainEvent
{
    public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
    {
        Task HandleAsync(TDomainEvent query, CancellationToken cancellation);
    }
}
