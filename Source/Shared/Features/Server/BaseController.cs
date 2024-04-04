using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Features.Messaging.Command;
using Shared.Features.Messaging.DomainEvent;
using Shared.Features.Messaging.IntegrationEvent;
using Shared.Features.Messaging.Query;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.Services.ModelValidation;

namespace Shared.Features.Server
{
    public class BaseController : ControllerBase
    {
        protected readonly IExecutionContext executionContext;
        protected readonly ICommandDispatcher commandDispatcher;
        protected readonly IQueryDispatcher queryDispatcher;
        protected readonly IIntegrationEventDispatcher integrationEventDispatcher;
        protected readonly IDomainEventDispatcher domainEventDispatcher;
        protected readonly IValidationService validationService;

        public BaseController(IServiceProvider serviceProvider)
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
