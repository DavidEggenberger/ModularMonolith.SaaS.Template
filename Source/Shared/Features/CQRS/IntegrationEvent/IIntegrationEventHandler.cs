using Shared.Features.Server.ExecutionContext;
using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.CQRS.IntegrationEvent
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IInServerExecutionContextScope where TIntegrationEvent : IIntegrationEvent
    {
        Task HandleAsync(TIntegrationEvent integrationEvent, CancellationToken cancellation);
    }
}
