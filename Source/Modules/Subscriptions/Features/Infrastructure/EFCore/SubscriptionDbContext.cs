using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate;

namespace Modules.Subscription.Features.Infrastructure.EFCore
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Subscription");

            base.OnModelCreating(modelBuilder);
        }
    }
}
