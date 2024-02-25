using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Subscriptions.DomainFeatures.Agregates.StripeSubscriptionAggregate;

namespace Modules.Subscriptions.DomainFeatures.StripeSubscriptionAggregate.Infrastructure
{
    public class StripeSubscriptionEFConfiguration : IEntityTypeConfiguration<StripeSubscription>
    {
        public void Configure(EntityTypeBuilder<StripeSubscription> builder)
        {

        }
    }
}
