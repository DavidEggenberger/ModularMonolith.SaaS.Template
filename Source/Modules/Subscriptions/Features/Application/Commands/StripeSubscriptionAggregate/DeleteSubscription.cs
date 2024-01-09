using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.Application.Commands.StripeSubscriptionAggregate
{
    public class DeleteSubscription : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }
}
