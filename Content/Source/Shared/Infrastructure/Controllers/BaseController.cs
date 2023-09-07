using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.CQRS.Command;
using Shared.Infrastructure.CQRS.Query;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.ModelValidation;
using System;

namespace Shared.Infrastructure
{
    public class BaseController : ControllerBase
    {
        protected readonly ICommandDispatcher commandDispatcher;
        protected readonly IQueryDispatcher queryDispatcher;
        protected readonly IExecutionContextAccessor executionContextAccessor;
        protected readonly IWebContextAccessor webContextAccessor;
        protected readonly IValidationService validationService;

        public BaseController(IServiceProvider serviceProvider)
        {
            commandDispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
            queryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();
            executionContextAccessor = serviceProvider.GetRequiredService<IExecutionContextAccessor>();
            webContextAccessor = serviceProvider.GetService<IWebContextAccessor>();
            validationService = serviceProvider.GetRequiredService<IValidationService>();
        }
    }
}
