using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks.ModelValidation;

namespace Shared.Features.Server
{
    public class BaseController : ControllerBase, InServerExecutionScopeBase
    {
        public IServerExecutionContext ExecutionContext { get; }

        protected readonly IValidationService validationService;

        public BaseController(IServiceProvider serviceProvider)
        {
            ExecutionContext = serviceProvider.GetRequiredService<IServerExecutionContext>();
            validationService = serviceProvider.GetRequiredService<IValidationService>();
        }
    }
}
