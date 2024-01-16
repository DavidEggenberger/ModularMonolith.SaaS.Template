using Shared.Features.CQRS.Query;

namespace Modules.Subscriptions.Features.Domain.StripeSubscriptionAggregate.Queries
{
    public class GetStripeCheckoutSession : IQuery<Stripe.Checkout.Session>
    {
        public string SessionId { get; set; }
    }
}
