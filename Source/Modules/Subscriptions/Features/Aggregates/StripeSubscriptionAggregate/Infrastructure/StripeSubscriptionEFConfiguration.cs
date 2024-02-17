using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate;

namespace Modules.Subscriptions.Features.Aggregates.StripeSubscriptionAggregate.Infrastructure
{
    public class StripeSubscriptionEFConfiguration : IEntityTypeConfiguration<StripeSubscription>
    {
        public void Configure(EntityTypeBuilder<StripeSubscription> builder)
        {

        }
    }
}
