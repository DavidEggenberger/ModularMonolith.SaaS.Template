using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Server.BuildingBlocks.Swagger
{
    public static class APIVersioningDIRegistrator
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
    }
}
