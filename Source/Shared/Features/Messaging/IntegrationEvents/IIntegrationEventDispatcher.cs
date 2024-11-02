using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.Messaging.IntegrationEvents
{
    public interface IIntegrationEventDispatcher
    {
        Task RaiseAndWaitForCompletionAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellation = default) where TIntegrationEvent : IIntegrationEvent;
    }
}
