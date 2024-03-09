using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Server.BuildingBlocks.Logging
{
    public static class Registrator
    {
        public static IServiceCollection Add_Logging(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddHttpLogging(options =>
            {
            });
        }

        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder application)
        {
            return application.UseHttpLogging();
        }
    }
}
