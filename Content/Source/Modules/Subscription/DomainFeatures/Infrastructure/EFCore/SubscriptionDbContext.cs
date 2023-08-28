using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Modules.Subscription.DomainFeatures.StripeSubscriptionAggregate.Domain;
using Shared.Infrastructure.EFCore;
using Shared.Infrastructure.EFCore.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.Infrastructure.EFCore
{
    public class SubscriptionDbContext : DbContext
    {
        public SubscriptionDbContext()
        {
            
        }
        public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<StripeCustomer> StripeCustomers { get; set; }
        public DbSet<StripeSubscription> Subscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("afasdfasfsdafdafsasdfasdfadsf");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Subscription");

            base.OnModelCreating(modelBuilder);
        }
    }
}
