using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.Features.Agregates.StripeCustomerAggregate;
using Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate;
using Shared.Features.EFCore;

namespace Modules.Subscription.Features.Infrastructure.EFCore
{
    public class SubscriptionsDbContext : BaseDbContext<SubscriptionsDbContext>
    {
        public SubscriptionsDbContext(DbContextOptions<SubscriptionsDbContext> dbContextOptions, IServiceProvider serviceProvider = null) : base(serviceProvider, "Subscriptions", dbContextOptions)
        {
            
        }

        public DbSet<StripeCustomer> StripeCustomers { get; set; }
        public DbSet<StripeSubscription> StripeSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Subscription");

            base.OnModelCreating(modelBuilder);
        }
    }
}
