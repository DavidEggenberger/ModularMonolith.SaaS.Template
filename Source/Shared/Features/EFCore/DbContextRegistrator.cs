using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.EFCore.Configuration;

namespace Shared.Features.EFCore
{
    public static class DbContextRegistrator
    {
        public static void RegisterDbContext<T>(this IServiceCollection services, string schemaName) where T : BaseDbContext<T>
        {
            var serviceProvider = services.BuildServiceProvider();

            services.AddDbContext<T>(options =>
            {
                var connectionString = serviceProvider.GetRequiredService<EFCoreConfiguration>().SQLServerConnectionString;
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(5);
                    sqlServerOptions.CommandTimeout(15);
                    sqlServerOptions.MigrationsHistoryTable($"{schemaName}_MigrationHistory");
                });
            });

            if (serviceProvider.GetRequiredService<IHostEnvironment>().IsProduction())
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
