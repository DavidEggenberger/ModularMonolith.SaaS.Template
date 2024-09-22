using DbUp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.EFCore.Configuration;
using Shared.Kernel.BuildingBlocks;
using System.Reflection;

namespace Shared.Features.EFCore.DbUp
{
    public static class Registrator
    {
        public static async Task<IServiceCollection> AddDbUpMigration(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();

            var isProduction = serviceProvider.GetRequiredService<IExecutionContext>().HostingEnvironment.IsProduction();
            var efCoreConfiguration = serviceProvider.GetRequiredService<EFCoreConfiguration>();
            var connectionString = isProduction ? efCoreConfiguration.SQLServerConnectionString_Prod : efCoreConfiguration.SQLServerConnectionString_Dev;

            var upgrader = DeployChanges.To
                               .SqlDatabase(connectionString)
                               .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                               .JournalToSqlTable("dbo", "MigrationHistory_DbUP")
                               .LogToConsole()
                               .Build();

            if (upgrader.TryConnect(out string _) is false)
            {
                var sqlConnectionParameters = connectionString.Split(';');
                var databaseName = sqlConnectionParameters[1].Split("=")[1];
                var serverConnectionString = string.Join(";", sqlConnectionParameters.Where(e => e.StartsWith("Database") is false));

                using (var connection = new SqlConnection(string.Join("", serverConnectionString)))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = $"CREATE DATABASE {databaseName}";
                    await command.ExecuteNonQueryAsync();
                }
            }

            var scripts = upgrader.GetScriptsToExecute();
            foreach (var script in scripts)
            {
                Console.WriteLine(script.Name);
            }

            upgrader.PerformUpgrade();

            return services;
        }
    }
}
