using Shared.Features.CQRS.Command;
using Shared.Features.CQRS.DomainEvent;
using Shared.Features.CQRS.IntegrationEvent;
using Shared.Features.CQRS.Query;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.ModelValidation;

namespace Shared.Features.Server
{
    public interface IInServerExecutionScope
    {
        public IExecutionContext ExecutionContext { get; }
        public ICommandDispatcher CommandDispatcher { get; }
        public IQueryDispatcher QueryDispatcher { get; }
        public IIntegrationEventDispatcher IntegrationEventDispatcher { get; }
        public IDomainEventDispatcher DomainEventDispatcher { get; }
        public IValidationService ValidationService { get; }
    }
}
