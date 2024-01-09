using Shared.Features.CQRS.Query;

namespace Modules.Subscriptions.Features.Application.Queries.StripeSubscriptionAggregate
{
    public class GetStripeCheckoutSession : IQuery<Stripe.Checkout.Session>
    {
        public string SessionId { get; set; }
    }
}
