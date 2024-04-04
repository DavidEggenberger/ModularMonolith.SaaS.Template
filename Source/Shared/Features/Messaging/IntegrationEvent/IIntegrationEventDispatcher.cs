using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.Messaging.IntegrationEvent
{
    public interface IIntegrationEventDispatcher
    {
        Task RaiseAndWaitForCompletionAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellation = default) where TIntegrationEvent : IIntegrationEvent;
    }
}
