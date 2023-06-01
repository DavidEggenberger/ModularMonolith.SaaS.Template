using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.CQRS.Command;
using Shared.Infrastructure.CQRS.Query;
using System;

namespace Shared.Web.Server
{
    public class BaseController : ControllerBase
    {
        protected readonly ICommandDispatcher commandDiscpatcher;
        protected readonly IQueryDispatcher queryDispatcher;

        public BaseController(IServiceProvider serviceProvider)
        {
            commandDiscpatcher = serviceProvider.GetRequiredService<ICommandDispatcher>();
            queryDispatcher = serviceProvider.GetRequiredService<IQueryDispatcher>();
        }


    }
}
