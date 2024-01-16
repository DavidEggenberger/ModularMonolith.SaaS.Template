using Shared.Kernel.BuildingBlocks.Events;

namespace Shared.Features.CQRS.IntegrationEvent
{
    public interface IIntegrationEventDispatcher
    {
        Task RaiseAndWaitForCompletionAsync<TIntegrationEvent>(TIntegrationEvent command, CancellationToken cancellation = default) where TIntegrationEvent : IIntegrationEvent;
    }
}
