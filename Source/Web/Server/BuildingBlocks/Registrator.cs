using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Web.Server.BuildingBlocks.APIVersioning;
using Web.Server.BuildingBlocks.ExceptionHandling;
using Web.Server.BuildingBlocks.Logging;
using Web.Server.BuildingBlocks.ModelValidation;
using Web.Server.BuildingBlocks.SecurityHeaders;
using Web.Server.BuildingBlocks.Swagger;
using WebServer.Modules.AntiforgeryToken;

namespace Web.Server.BuildingBlocks
{
    public static class Registrator
    {
        public static IServiceCollection AddBuildingBlocks(this IServiceCollection services)
        {
            services.AddAntiforgeryToken();
            services.Add_ApiVersioning();
            services.Add_Logging();
            services.AddModelValidation();
            services.AddSwagger();

            return services;
        }

        public static IApplicationBuilder UseBuildingBlocksMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseApiVersioningMiddleware();
            applicationBuilder.UseExceptionHandlingMiddleware();
            applicationBuilder.UseLoggingMiddleware();
            applicationBuilder.UseSecurityHeadersMiddleware();
            applicationBuilder.UseSwaggerMiddleware();

            return applicationBuilder;
        }
    }
}
