using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Modules.Subscription.DomainFeatures.Domain;
using Shared.Infrastructure.EFCore;
using Shared.Infrastructure.MultiTenancy.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.Infrastructure.EFCore
{
    public class SubscriptionDbContext : BaseDbContext<SubscriptionDbContext>
    {
        private readonly IHostEnvironment hostEnvironment;
        private readonly IConfiguration configuration;

        public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> dbContextOptions, IServiceProvider serviceProvider, IConfiguration configuration, IHostEnvironment hostEnvironment) : base(dbContextOptions, serviceProvider, configuration)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public DbSet<StripeSubscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Subscription");
        }
    }
}
