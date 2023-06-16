using Shared.DomainFeatures.DomainKernel.Events;

namespace Shared.Infrastructure.CQRS.IntegrationEvent
{
    public interface IIntegrationEventDispatcher
    {
        void Raise<TDomainEvent>(TDomainEvent command, CancellationToken cancellation) where TDomainEvent : IDomainEvent;
    }
}
