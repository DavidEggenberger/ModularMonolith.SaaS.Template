using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.Features.DomainFeatures.StripeCustomers;
using Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions;
using Shared.Features.EFCore;

namespace Modules.Subscriptions.Features.Infrastructure.EFCore
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
