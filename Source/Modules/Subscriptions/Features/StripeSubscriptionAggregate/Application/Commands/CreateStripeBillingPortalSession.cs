using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.StripeSubscriptionAggregate.Application.Commands
{
    public class CreateStripeBillingPortalSession : ICommand<Stripe.BillingPortal.Session>
    {
        public string StripeCustomerId { get; set; }
        public string RedirectBaseUrl { get; set; }
    }
}
