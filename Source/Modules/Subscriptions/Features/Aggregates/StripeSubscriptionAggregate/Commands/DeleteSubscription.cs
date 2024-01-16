using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.Domain.StripeSubscriptionAggregate.Commands
{
    public class DeleteSubscription : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }
}
