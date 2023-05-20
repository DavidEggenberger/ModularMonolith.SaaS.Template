using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
                //[typeof(IdentityOperationException)] = HandleIdentityOperationExceptionAsync,
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

        private async Task<ActionResult> HandleIdentityOperationExceptionAsync(Exception exception)
        {
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An internal server error ocurred"
            };
            return Problem(exception.Message);
        }
        private async Task<ActionResult> HandleUnknownExceptionAsync(Exception exception)
        {
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An internal server error ocurred"
            };
            return Problem();
        }
    }
}
