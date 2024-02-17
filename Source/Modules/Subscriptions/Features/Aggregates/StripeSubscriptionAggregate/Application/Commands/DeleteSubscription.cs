using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.Aggregates.StripeSubscriptionAggregate.Application.Commands
{
    public class DeleteSubscription : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }
}
