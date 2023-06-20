using Microsoft.AspNetCore.Builder;

namespace Web.Server.BuildingBlocks.Swagger
{
    public static class APIVersioningMdwRegistrator
    {
        public static IApplicationBuilder RegisterApiVersioning(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseApiVersioning();

            return applicationBuilder;
        }
    }
}
