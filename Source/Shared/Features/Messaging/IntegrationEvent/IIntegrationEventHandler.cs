using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.Messaging.IntegrationEvent
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> where TIntegrationEvent : IIntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent integrationEvent, CancellationToken cancellation);
    }
}
