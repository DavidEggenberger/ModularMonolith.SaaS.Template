using Shared.DomainFeatures.DomainKernel.Events;

namespace Shared.Infrastructure.CQRS.IntegrationEvent
{
    public interface IIntegrationEventDispatcher
    {
        void Raise<TIIntegrationEvent>(TIIntegrationEvent command, CancellationToken cancellation) where TIIntegrationEvent : IIntegrationEvent;
    }
}
