using Shared.Features.CQRS.Command;
using Shared.Features.CQRS.DomainEvent;
using Shared.Features.CQRS.IntegrationEvent;
using Shared.Features.CQRS.Query;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.ModelValidation;

namespace Shared.Features.Server
{
    public class InServerExecutionScopeBase : IInServerExecutionScope
    {
        public IExecutionContext ExecutionContext { get; private set; }
        public ICommandDispatcher CommandDispatcher { get; private set; }
        public IQueryDispatcher QueryDispatcher { get; private set; }
        public IIntegrationEventDispatcher IntegrationEventDispatcher { get; private set; }
        public IDomainEventDispatcher DomainEventDispatcher { get; private set; }
        public IValidationService ValidationService { get; private set; }
    }
}
