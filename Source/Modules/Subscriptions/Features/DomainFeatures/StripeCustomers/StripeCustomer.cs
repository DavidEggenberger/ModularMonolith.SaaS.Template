using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shared.Features.Misc;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeCustomers
{
    public class StripeCustomer : Entity
    {
        public Guid UserId { get; private set; }
        public string StripePortalCustomerId { get; private set; }

        public static StripeCustomer Create(Guid userId, string stripePortalCustomerId)
        {
            return new StripeCustomer
            {
                StripePortalCustomerId = stripePortalCustomerId,
                UserId = userId,
            };
        }
    }

    public class StripeCustomerEFConfiguration : IEntityTypeConfiguration<StripeCustomer>
    {
        public void Configure(EntityTypeBuilder<StripeCustomer> builder)
        {
            builder.ToTable(nameof(StripeCustomer));
        }
    }
}
