using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptionAggregate.Infrastructure
{
    public class StripeSubscriptionEFConfiguration : IEntityTypeConfiguration<StripeSubscription>
    {
        public void Configure(EntityTypeBuilder<StripeSubscription> builder)
        {

        }
    }
}
