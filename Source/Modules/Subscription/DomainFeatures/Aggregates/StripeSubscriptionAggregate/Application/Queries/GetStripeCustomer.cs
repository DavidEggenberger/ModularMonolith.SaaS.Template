using Modules.Subscription.Features.Aggregates.StripeSubscriptionAggregate.Domain;
using Shared.Features.CQRS.Query;

namespace Modules.Subscription.Features.Aggregates.StripeSubscriptionAggregate.Application.Queries
{
    public class GetStripeCustomer : IQuery<StripeCustomer>
    {
        public string StripeCustomerId { get; set; }
    }
}
