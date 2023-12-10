using Shared.DomainFeatures.CQRS.Command;

namespace Modules.Subscription.DomainFeatures.Aggregates.StripeSubscriptionAggregate.Application.Commands
{
    public class CreateStripeBillingPortalSession : ICommand<Stripe.BillingPortal.Session>
    {
        public string StripeCustomerId { get; set; }
        public string RedirectBaseUrl { get; set; }
    }
}
