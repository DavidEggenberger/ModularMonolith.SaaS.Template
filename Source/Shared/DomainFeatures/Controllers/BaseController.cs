using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.DomainFeatures.CQRS.Command;
using Shared.DomainFeatures.CQRS.Query;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.ModelValidation;

namespace Shared.DomainFeatures
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
