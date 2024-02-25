using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.DomainFeatures.Agregates.StripeCustomerAggregate;
using Modules.Subscriptions.DomainFeatures.Agregates.StripeSubscriptionAggregate;
using Shared.Features.EFCore;

namespace Modules.Subscription.DomainFeatures.Infrastructure.EFCore
{
    public class SubscriptionsDbContext : BaseDbContext<SubscriptionsDbContext>
    {
        public SubscriptionsDbContext(DbContextOptions<SubscriptionsDbContext> dbContextOptions, IServiceProvider serviceProvider = null) : base(serviceProvider, "Subscriptions", dbContextOptions)
        {
            
        }

        public DbSet<StripeCustomer> StripeCustomers { get; set; }
        public DbSet<StripeSubscription> StripeSubscriptions { get; set; }
    }
}
