using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Features.CQRS.Command;
using Shared.Features.CQRS.Query;
using Shared.Kernel.BuildingBlocks.ContextAccessors;
using Shared.Kernel.BuildingBlocks.ModelValidation;

namespace Shared.Features
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
