using Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate;
using Shared.Features.CQRS.Query;

namespace Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate.Application.Queries
{
    public class GetStripeCustomer : IQuery<StripeCustomer>
    {
        public string StripeCustomerId { get; set; }
    }
}
