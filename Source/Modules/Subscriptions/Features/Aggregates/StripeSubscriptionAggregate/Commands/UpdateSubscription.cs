using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.Domain.StripeSubscriptionAggregate.Commands
{
    public class UpdateSubscription : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }
}
