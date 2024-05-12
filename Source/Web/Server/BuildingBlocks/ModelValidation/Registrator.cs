using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks.Services.ModelValidation;

namespace Web.Server.BuildingBlocks.ModelValidation
{
    public static class Registrator
    {
        public static IServiceCollection AddModelValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidationService, ValidationService>();

            return services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Type = $"https://httpstatuses.com/400",
                        Detail = "ApiConstants.Messages.ModelStateValidation"
                    };
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes =
                        {
                            "ApiConstants.ContentTypes.ProblemJson",
                            "ApiConstants.ContentTypes.ProblemXml"
                        }
                    };
                };
            });
        }
    }
}
