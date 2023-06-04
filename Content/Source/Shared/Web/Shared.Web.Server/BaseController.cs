using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.CQRS.Command;
using Shared.Infrastructure.CQRS.Query;
using Shared.Kernel.BuildingBlocks;
using System;

namespace Shared.Web.Server
{
    public class BaseController : ControllerBase
    {
        protected readonly ICommandDispatcher commandDispatcher;
        protected readonly IQueryDispatcher queryDispatcher;
        protected readonly IExecutionContextAccessor executionContextAccessor;
        public BaseController(IServiceProvider serviceProvider)
        {
            commandDispatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
            queryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();
            executionContextAccessor = serviceProvider.GetRequiredService<IExecutionContextAccessor>();
        }
    }
}
