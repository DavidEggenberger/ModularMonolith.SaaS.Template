using Shared.DomainFeatures.DomainKernel.Events;

namespace Shared.Infrastructure.CQRS.IntegrationEvent
{
    public interface IIntegrationEventHandler<in TIIntegrationEvent> where TIIntegrationEvent : IIntegrationEvent
    {
        Task HandleAsync(TIIntegrationEvent integrationEvent, CancellationToken cancellation);
    }
}
