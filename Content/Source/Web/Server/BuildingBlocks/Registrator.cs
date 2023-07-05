using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Web.Server.BuildingBlocks.HostingInformation;
using Web.Server.BuildingBlocks.Logging;
using Web.Server.BuildingBlocks.ModelValidation;
using Web.Server.BuildingBlocks.Swagger;
using WebServer.Modules.ModelValidation;
using WebServer.Modules.Swagger;

namespace Web.Server.BuildingBlocks
{
    public static class Registrator
    {
        public static IServiceCollection RegisterBuildingBlocks(this IServiceCollection services)
        {
            services.RegisterAntiforgeryToken();
            services.RegisterApiVersioning();
            services.RegisterLogging();
            services.RegisterModelValidation();
            services.RegisterSwagger();
            services.RegisterWebContextAccessor();
            services.RegisterModelValidation();

            return services;
        }

        public static IApplicationBuilder RegisterBuildingBlocks(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.RegisterApiVersioning();
            applicationBuilder.RegisterExceptionHandling();
            applicationBuilder.RegisterLogging();
            applicationBuilder.RegisterSecurityHeaders();
            applicationBuilder.RegisterSwagger();

            return applicationBuilder;
        }
    }
}
