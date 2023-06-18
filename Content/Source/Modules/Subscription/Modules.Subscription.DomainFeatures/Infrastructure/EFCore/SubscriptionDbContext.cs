using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modules.Subscription.DomainFeatures.Domain;
using Shared.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.Infrastructure.EFCore
{
    public class SubscriptionDbContext : BaseDbContext<SubscriptionDbContext>
    {
        public SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> dbContextOptions, IServiceProvider serviceProvider, IConfiguration configuration) : base(dbContextOptions, serviceProvider, configuration)
        {
        }

        public DbSet<StripeSubscription> Subscriptions { get; set; }
    }
}
