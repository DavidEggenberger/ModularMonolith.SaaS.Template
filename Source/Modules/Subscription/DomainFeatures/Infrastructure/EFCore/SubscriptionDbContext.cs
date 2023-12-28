﻿using Microsoft.EntityFrameworkCore;
using Modules.Subscription.Features.Aggregates.StripeSubscriptionAggregate.Domain;

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
