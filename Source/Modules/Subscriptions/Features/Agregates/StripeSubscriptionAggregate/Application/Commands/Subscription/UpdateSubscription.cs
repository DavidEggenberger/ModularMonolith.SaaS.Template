using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate.Application.Commands.Subscription
{
    public class UpdateSubscription : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }
}
