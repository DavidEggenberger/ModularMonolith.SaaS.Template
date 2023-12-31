using Shared.Kernel.BuildingBlocks.Events;

namespace Shared.Features.CQRS.IntegrationEvent
{
    public interface IIntegrationEventDispatcher
    {
        void Raise<TIntegrationEvent>(TIntegrationEvent command, CancellationToken cancellation) where TIntegrationEvent : IIntegrationEvent;

        //Task<TResult> RaiseAndWaitForResultAsync<TIntegrationEvent, TResult>(TIntegrationEvent command, CancellationToken cancellation = default) where TIntegrationEvent : IIntegrationEvent<TResult>;
    }
}
