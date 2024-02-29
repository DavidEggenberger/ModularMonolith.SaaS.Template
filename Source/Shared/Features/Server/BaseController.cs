using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Features.Server.ExecutionContext;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.ModelValidation;

namespace Shared.Features.Server
{
    public class BaseController : ControllerBase, IInServerExecutionContextScope
    {
        public IExecutionContext ExecutionContext { get; init; }

        protected readonly IValidationService validationService;

        public BaseController(IServiceProvider serviceProvider)
        {
            ExecutionContext = serviceProvider.GetRequiredService<IExecutionContext>();
            validationService = serviceProvider.GetRequiredService<IValidationService>();
        }
    }
}
