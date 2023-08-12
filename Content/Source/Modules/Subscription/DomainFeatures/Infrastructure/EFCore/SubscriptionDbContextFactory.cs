using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shared.Infrastructure.EFCore.Configuration;

namespace Modules.Subscription.DomainFeatures.Infrastructure.EFCore
{
    public class SubscriptionDbContextFactory : IDesignTimeDbContextFactory<SubscriptionDbContext>
    {
        private readonly IHostEnvironment hostEnvironment;
        private readonly IOptions<EFCoreConfiguration> configuration;

        public SubscriptionDbContextFactory()
        {
                
        }

        public SubscriptionDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SubscriptionDbContext>();

            if (hostEnvironment.IsDevelopment())
            {
                optionsBuilder.UseSqlServer(configuration.Value.DevelopmentSQLServerConnectionString);
            }
            if (hostEnvironment.IsProduction())
            {
                optionsBuilder.UseSqlServer(configuration.Value.ProductionSQLServerConnectionString);
            }

            return new SubscriptionDbContext(optionsBuilder.Options);
        }
    }
}
