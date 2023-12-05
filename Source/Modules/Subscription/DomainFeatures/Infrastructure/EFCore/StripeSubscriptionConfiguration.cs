using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Subscription.DomainFeatures.Aggregates.StripeSubscriptionAggregate.Domain;

namespace Modules.Subscription.DomainFeatures.Infrastructure.EFCore
{
    public class StripeSubscriptionConfiguration : IEntityTypeConfiguration<StripeSubscription>
    {
        public void Configure(EntityTypeBuilder<StripeSubscription> builder)
        {
            
        }
    }
}
