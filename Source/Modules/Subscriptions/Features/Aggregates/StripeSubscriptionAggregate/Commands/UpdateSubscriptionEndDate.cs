using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.Domain.StripeSubscriptionAggregate.Commands
{
    public class UpdateSubscriptionEndDate : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }
}
