using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.Features.DomainFeatures.Agregates.StripeCustomerAggregate;
using Modules.Subscriptions.Features.DomainFeatures.Agregates.StripeSubscriptionAggregate;
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
    }
}
