using Shared.Infrastructure.CQRS.Command;
using Shared.Kernel.BuildingBlocks.Events;

namespace Shared.Infrastructure.CQRS.IntegrationEvent
{
    public interface IIntegrationEventDispatcher
    {
        void Raise<TIntegrationEvent>(TIntegrationEvent command, CancellationToken cancellation) where TIntegrationEvent : IIntegrationEvent;

        //Task<TResult> RaiseAndWaitForResultAsync<TIntegrationEvent, TResult>(TIntegrationEvent command, CancellationToken cancellation = default) where TIntegrationEvent : IIntegrationEvent<TResult>;
    }
}
