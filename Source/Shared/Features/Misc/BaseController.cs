﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Features.Messaging.Commands;
using Shared.Features.Messaging.IntegrationEvents;
using Shared.Features.Messaging.Queries;
using Shared.Features.Misc.ExecutionContext;
using Shared.Kernel.BuildingBlocks.Services.ModelValidation;

namespace Shared.Features.Misc
{
    public class BaseController<TModule> : BaseController
    {
        protected readonly TModule module;

        public BaseController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            module = serviceProvider.GetService<TModule>();
        }
    }

    public class BaseController : ControllerBase
    {
        protected readonly IExecutionContext executionContext;
        protected readonly ICommandDispatcher commandDispatcher;
        protected readonly IQueryDispatcher queryDispatcher;
        protected readonly IIntegrationEventDispatcher integrationEventDispatcher;
        protected readonly IValidationService validationService;

        public BaseController(IServiceProvider serviceProvider)
        {
            executionContext = serviceProvider.GetRequiredService<IExecutionContext>();
            commandDispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
            queryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();
            integrationEventDispatcher = serviceProvider.GetRequiredService<IIntegrationEventDispatcher>();
            validationService = serviceProvider.GetRequiredService<IValidationService>();
        }
    }
}