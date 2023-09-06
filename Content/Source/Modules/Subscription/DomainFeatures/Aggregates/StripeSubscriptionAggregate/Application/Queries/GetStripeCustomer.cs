using Modules.Subscription.DomainFeatures.Aggregates.StripeSubscriptionAggregate.Domain;
using Shared.Infrastructure.CQRS.Query;

namespace Modules.Subscription.DomainFeatures.Aggregates.StripeSubscriptionAggregate.Application.Queries
{
    public class GetStripeCustomer : IQuery<StripeCustomer>
    {
        public string StripeCustomerId { get; set; }
    }
}
