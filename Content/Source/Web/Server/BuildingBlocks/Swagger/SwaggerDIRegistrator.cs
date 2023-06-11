using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Web.Server.BuildingBlocks.Swagger
{
    public static class SwaggerDIDIRegistrator
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CrispyCollab",
                    Description = "Swagger Documentation for CrispyCollab",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });
            });

            return serviceCollection;
        }
    }
}
