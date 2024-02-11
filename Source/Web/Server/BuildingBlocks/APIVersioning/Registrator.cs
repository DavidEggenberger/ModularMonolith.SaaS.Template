using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Server.BuildingBlocks.APIVersioning
{
    public static class Registrator
    {
        public static IServiceCollection RegisterApiVersioning(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new HeaderApiVersionReader("ApiVersion");
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            return serviceCollection;
        }

        public static IApplicationBuilder RegisterApiVersioning(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseApiVersioning();

            return applicationBuilder;
        }
    }
}
