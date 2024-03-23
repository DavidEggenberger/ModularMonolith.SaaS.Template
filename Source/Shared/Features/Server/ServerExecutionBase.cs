using Microsoft.Extensions.DependencyInjection;
using Shared.Features.CQRS.Command;
using Shared.Features.CQRS.DomainEvent;
using Shared.Features.CQRS.IntegrationEvent;
using Shared.Features.CQRS.Query;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.Services.ModelValidation;

namespace Shared.Features.Server
{
    public class ServerExecutionBase
    {
        protected readonly IExecutionContext executionContext;
        protected readonly ICommandDispatcher commandDispatcher;
        protected readonly IQueryDispatcher queryDispatcher;
        protected readonly IIntegrationEventDispatcher integrationEventDispatcher;
        protected readonly IDomainEventDispatcher domainEventDispatcher;
        protected readonly IValidationService validationService;

        public ServerExecutionBase(IServiceProvider serviceProvider)
        {
            executionContext = serviceProvider.GetRequiredService<IExecutionContext>();
            commandDispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
            queryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();
            integrationEventDispatcher = serviceProvider.GetRequiredService<IIntegrationEventDispatcher>();
            domainEventDispatcher = serviceProvider.GetRequiredService<IDomainEventDispatcher>();
            validationService = serviceProvider.GetRequiredService<IValidationService>();
        }
    }
}
