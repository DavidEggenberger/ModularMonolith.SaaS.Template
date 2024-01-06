using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Features.DomainKernel.Exceptions;
using Shared.Kernel.BuildingBlocks.Auth.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Server.BuildingBlocks.ExceptionHandling
{
    [Route("[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class ExceptionHandler : ControllerBase
    {
        private ILogger<ExceptionHandler> logger;
        private Dictionary<Type, Func<Exception, Task<ActionResult>>> exceptionHandlers;
        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            this.logger = logger;
            exceptionHandlers = new Dictionary<Type, Func<Exception, Task<ActionResult>>>
            {
                [typeof(NotFoundException)] = HandleNotFoundException,
                [typeof(UnauthorizedException)] = HandleUnauthorizedException
            };
        }

        [HttpGet]
        public async Task<ActionResult> OnGet()
        {
            Exception exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
            logger.LogError(exception.Message);

            Type type = exception.GetType();
            if (exceptionHandlers.ContainsKey(type))
            {
                return await exceptionHandlers[type].Invoke(exception);
            }
            else
            {
                return await HandleUnknownExceptionAsync(exception);
            }
        }

        private async Task<ActionResult> HandleNotFoundException(Exception exception)
        {
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = exception.Message
            };
            return new ObjectResult(problemDetails);
        }
        private async Task<ActionResult> HandleUnauthorizedException(Exception exception)
        {
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = exception.Message
            };
            return new ObjectResult(exception.Message);
        }
        private async Task<ActionResult> HandleUnknownExceptionAsync(Exception exception)
        {
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An internal server error ocurred"
            };
            return new ObjectResult(exception);
        }
    }
}
