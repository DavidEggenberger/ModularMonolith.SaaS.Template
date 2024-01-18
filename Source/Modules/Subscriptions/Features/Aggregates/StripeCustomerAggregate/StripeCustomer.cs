using Shared.Features.DomainKernel;

namespace Modules.Subscriptions.Features.Agregates.StripeCustomerAggregate
{
    public class StripeCustomer : AggregateRoot
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
}
