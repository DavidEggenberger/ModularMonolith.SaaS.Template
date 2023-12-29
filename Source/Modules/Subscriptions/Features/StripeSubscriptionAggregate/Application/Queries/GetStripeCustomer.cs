using Shared.Features.CQRS.Query;

namespace Modules.Subscriptions.Features.StripeSubscriptionAggregate.Application.Queries
{
    public class GetStripeCustomer : IQuery<StripeCustomer>
    {
        public string StripeCustomerId { get; set; }
    }
}
