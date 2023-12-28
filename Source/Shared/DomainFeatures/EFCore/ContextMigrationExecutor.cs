using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Features.EFCore
{
    public static class ContextMigrationExecutor
    {
        public static IServiceCollection MigrateContext<T>(this IServiceCollection services) where T : DbContext
        {
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var db = scope.ServiceProvider.GetService<T>();
                db.Database.Migrate();
            }

            return services;
        }
    }
}
