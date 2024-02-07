using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.EFCore
{
    public static class DbContextRegistrator
    {
        public static void RegisterDbContext<T>(this IServiceCollection services, string schemaName) where T : DbContext
        {
            var serviceProvider = services.BuildServiceProvider();
            var executionContext = serviceProvider.GetRequiredService<IExecutionContext>();

            services.AddDbContext<T>();

            if (executionContext.HostingEnvironment.IsProduction())
            {
                using (var scope = services.BuildServiceProvider().CreateScope())
                {
                    var db = scope.ServiceProvider.GetService<T>();
                    db.Database.Migrate();
                }
            }
        }
    }
}
