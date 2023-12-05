using Shared.Kernel.BuildingBlocks.Events;

namespace Shared.Infrastructure.CQRS.IntegrationEvent
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> where TIntegrationEvent : IIntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent integrationEvent, CancellationToken cancellation);
    }
}
