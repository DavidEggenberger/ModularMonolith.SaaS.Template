using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.EFCore.Configuration;
using Shared.Kernel.BuildingBlocks;

namespace Shared.Features.EFCore
{
    public static class DbContextRegistrator
    {
        public static void RegisterDbContext<T>(this IServiceCollection services, string schemaName) where T : DbContext
        {
            var serviceProvider = services.BuildServiceProvider();

            services.AddDbContext<T>(options =>
            {
                var executionContext = serviceProvider.GetRequiredService<IExecutionContext>();
                var efCoreConfiguration = serviceProvider.GetRequiredService<EFCoreConfiguration>();

                options.UseSqlServer(
                    executionContext.HostingEnvironment.IsProduction() ? efCoreConfiguration.SQLServerConnectionString_Prod : efCoreConfiguration.SQLServerConnectionString_Dev,
                    sqlServerOptions =>
                    {
                        sqlServerOptions.EnableRetryOnFailure(5);
                        sqlServerOptions.CommandTimeout(15);
                        sqlServerOptions.MigrationsHistoryTable($"dbo.{schemaName}_MigrationHistory");
                    }
                );
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
