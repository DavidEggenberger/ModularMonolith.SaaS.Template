using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Server.BuildingBlocks.Logging
{
    public static class Registrator
    {
        public static IServiceCollection RegisterLogging(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddHttpLogging(options =>
            {
            });
        }

        public static IApplicationBuilder RegisterLogging(this IApplicationBuilder application)
        {
            return application.UseHttpLogging();
        }
    }
}
