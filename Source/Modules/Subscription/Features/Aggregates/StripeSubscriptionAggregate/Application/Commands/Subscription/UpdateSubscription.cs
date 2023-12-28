using Shared.Features.CQRS.Command;

namespace Modules.Subscription.Features.Aggregates.StripeSubscriptionAggregate.Application.Commands.Subscription
{
    public class UpdateSubscription : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }
}
