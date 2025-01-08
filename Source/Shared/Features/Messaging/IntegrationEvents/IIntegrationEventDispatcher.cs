using Shared.Kernel.BuildingBlocks.IntegrationMessages;

namespace Shared.Features.Messaging.IntegrationEvents
{
    public interface IIntegrationEventDispatcher
    {
        Task RaiseAndWaitForCompletionAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellation = default) where TIntegrationEvent : IIntegrationEvent;
    }
}
