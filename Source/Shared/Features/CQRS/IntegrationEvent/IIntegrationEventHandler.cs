using Shared.Features.Server;
using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.CQRS.IntegrationEvent
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : ServerExecutionBase where TIntegrationEvent : IIntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent integrationEvent, CancellationToken cancellation);
    }
}
