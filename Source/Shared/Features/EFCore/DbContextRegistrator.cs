using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.EFCore
{
    public static class DbContextRegistrator
    {
        public static void RegisterDbContext<T>(this IServiceCollection services) where T : DbContext
        {
            services.AddDbContext<T>();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                if (serviceProvider.GetRequiredService<IExecutionContext>().HostingEnvironment.IsProduction())
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetService<T>();
                        db.Database.Migrate();
                    }
                }
            }
        }
    }
}
