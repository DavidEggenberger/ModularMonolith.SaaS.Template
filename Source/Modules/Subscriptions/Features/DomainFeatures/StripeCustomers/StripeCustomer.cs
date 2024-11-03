using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions;
using Shared.Features.Domain;

namespace Modules.Subscriptions.Features.DomainFeatures.StripeCustomers
{
    public class StripeCustomer : Entity
    {
        public Guid UserId { get; set; }
        public string StripePortalCustomerId { get; set; }

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
