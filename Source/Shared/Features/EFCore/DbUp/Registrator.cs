using DbUp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.EFCore.Configuration;
using Shared.Kernel.BuildingBlocks;
using System.Reflection;

namespace Shared.Features.EFCore.DbUp
{
    public static class Registrator
    {
        public static IServiceCollection AddDbUpMigration(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();

            var isProduction = serviceProvider.GetRequiredService<IExecutionContext>().HostingEnvironment.IsProduction();
            var efCoreConfiguration = serviceProvider.GetRequiredService<EFCoreConfiguration>();

            var upgrader = DeployChanges.To
                               .SqlDatabase(isProduction ? efCoreConfiguration.SQLServerConnectionString_Prod : efCoreConfiguration.SQLServerConnectionString_Dev)
                               .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                               .JournalToSqlTable("dbo", "MigrationHistory")
                               .LogToConsole()
                               .Build();

            upgrader.PerformUpgrade();

            return services;
        }
    }
}
