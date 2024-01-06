using Modules.Subscriptions.Features.Agregates.StripeCustomerAggregate;
using Shared.Features.CQRS.Query;

namespace Modules.Subscriptions.Features.Agregates.StripeCustomerAggregate.Aplication.Queries
{
    public class GetStripeCustomer : IQuery<StripeCustomer>
    {
        public string StripeCustomerId { get; set; }
    }
}
