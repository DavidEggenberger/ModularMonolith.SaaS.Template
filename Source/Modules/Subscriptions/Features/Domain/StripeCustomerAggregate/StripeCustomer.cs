using Shared.Features.DomainKernel;

namespace Modules.Subscriptions.Features.Agregates.StripeCustomerAggregate
{
    public class StripeCustomer : AggregateRoot
    {
        public Guid UserId { get; set; }
        public string StripeCustomerId { get; set; }
    }
}
