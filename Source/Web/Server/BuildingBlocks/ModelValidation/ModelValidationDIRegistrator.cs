using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks.ModelValidation;

namespace Web.Server.BuildingBlocks.ModelValidation
{
    public static class ModelValidationDIRegistrator
    {
        public static IServiceCollection RegisterModelValidation(this IServiceCollection services)
        {
            services.RegisterModelValidationService();

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
