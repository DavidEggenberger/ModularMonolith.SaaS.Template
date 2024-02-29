using Shared.Features.CQRS.Command;
using Shared.Features.CQRS.DomainEvent;
using Shared.Features.CQRS.IntegrationEvent;
using Shared.Features.CQRS.Query;
using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.Server.ExecutionContext
{
    public interface IServerExecutionContext : IExecutionContext
    {
        public ICommandDispatcher CommandDispatcher { get; }
        public IQueryDispatcher QueryDispatcher { get; }
        public IIntegrationEventDispatcher IntegrationEventDispatcher { get; }
        public IDomainEventDispatcher DomainEventDispatcher { get; }
    }
}
