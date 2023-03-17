using Microsoft.AspNetCore.Builder;
using Server.BuildingBlocks.Controllers.Swagger;

namespace Server.BuildingBlocks
{
    public static class BuildingBlocksMdwRegistrator
    {
        public static void UseBuildingBlocks(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwaggerMiddleware();
        }
    }
}
