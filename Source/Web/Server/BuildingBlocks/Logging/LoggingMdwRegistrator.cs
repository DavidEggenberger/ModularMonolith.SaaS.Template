using Microsoft.AspNetCore.Builder;

namespace Web.Server.BuildingBlocks.Logging
{
    public static class LoggingMdwRegistrator
    {
        public static IApplicationBuilder RegisterLogging(this IApplicationBuilder application)
        {
            return application.UseHttpLogging();
        }
    }
}
