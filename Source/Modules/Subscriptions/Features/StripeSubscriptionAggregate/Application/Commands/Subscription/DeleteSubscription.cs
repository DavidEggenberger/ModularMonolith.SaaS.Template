using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.StripeSubscriptionAggregate.Application.Commands.Subscription
{
    public class DeleteSubscription : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }
}
