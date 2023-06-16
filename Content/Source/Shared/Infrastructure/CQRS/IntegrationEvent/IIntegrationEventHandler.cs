using Shared.DomainFeatures.DomainKernel.Events;

namespace Shared.Infrastructure.CQRS.IntegrationEvent
{
    public interface IIntegrationEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
    {
        Task HandleAsync(TDomainEvent query, CancellationToken cancellation);
    }
}
