using Shared.DomainFeatures.DomainKernel.Events;

namespace Shared.DomainFeatures.Infrastructure.CQRS.DomainEvent
{
    public interface IDomainEventDispatcher
    {
        Task RaiseAsync<TDomainEvent>(TDomainEvent command, CancellationToken cancellation) where TDomainEvent : IDomainEvent;
    }
}
