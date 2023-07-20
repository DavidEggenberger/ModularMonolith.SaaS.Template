using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Modules.Subscription.DomainFeatures.Domain;
using Shared.Infrastructure.EFCore;
using Shared.Infrastructure.EFCore.Configuration;
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
        public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> dbContextOptions, IServiceProvider serviceProvider, IHostEnvironment hostEnvironment) : base(dbContextOptions, serviceProvider)
        {
            
        }

        public DbSet<StripeCustomer> StripeCustomers { get; set; }
        public DbSet<StripeSubscription> Subscriptions { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Subscription");
        }
    }
}
