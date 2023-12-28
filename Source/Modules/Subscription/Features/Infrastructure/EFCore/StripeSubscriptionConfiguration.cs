using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Subscription.Features.Aggregates.StripeSubscriptionAggregate.Domain;

namespace Modules.Subscription.Features.Infrastructure.EFCore
{
    public class StripeSubscriptionConfiguration : IEntityTypeConfiguration<StripeSubscription>
    {
        public void Configure(EntityTypeBuilder<StripeSubscription> builder)
        {
            
        }
    }
}
