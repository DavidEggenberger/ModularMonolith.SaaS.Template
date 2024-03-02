using Microsoft.Extensions.DependencyInjection;
using Shared.Features.CQRS.DomainEvent;
using Shared.Features.CQRS.IntegrationEvent;
using Shared.Features.CQRS.Query;
using Shared.Kernel.BuildingBlocks.ModelValidation;
using Shared.Kernel.BuildingBlocks;
using Shared.Features.Server;

namespace Shared.Features.CQRS.Command
{
    public class BaseCommandHandler : ServerExecutionBase
    {
        public IExecutionContext ExecutionContext { get; }
        public ICommandDispatcher CommandDispatcher { get; }
        public IQueryDispatcher QueryDispatcher { get; }
        public IIntegrationEventDispatcher IntegrationEventDispatcher { get; }
        public IDomainEventDispatcher DomainEventDispatcher { get; }
        public IValidationService ValidationService { get; }

        public BaseCommandHandler(IServiceProvider serviceProvider)
        {
            ExecutionContext = serviceProvider.GetRequiredService<IExecutionContext>();
            CommandDispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
            QueryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();
            IntegrationEventDispatcher = serviceProvider.GetRequiredService<IIntegrationEventDispatcher>();
            DomainEventDispatcher = serviceProvider.GetRequiredService<IDomainEventDispatcher>();
            ValidationService = serviceProvider.GetRequiredService<IValidationService>();
        }
    }
}
