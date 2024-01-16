using Shared.Features.DomainKernel;

namespace Modules.Subscriptions.Features.Agregates.StripeCustomerAggregate
{
    public class StripeCustomer : AggregateRoot
    {
        public Guid UserId { get; set; }
        public string StripePortalCustomerId { get; set; }

        public static StripeCustomer Create(Guid tenantId, string stripePortalCustomerId)
        {
            return new StripeCustomer
            {
                CreatedAt = DateTime.UtcNow,
                StripePortalCustomerId = stripePortalCustomerId,
                TenantId = tenantId,
            };
        }
    }
}
