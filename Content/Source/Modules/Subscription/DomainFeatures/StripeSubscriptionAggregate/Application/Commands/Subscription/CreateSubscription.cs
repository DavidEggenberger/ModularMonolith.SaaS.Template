using Shared.Infrastructure.CQRS.Command;
using Stripe;

namespace Modules.Subscription.DomainFeatures.StripeSubscriptionAggregate.Application.Commands.Subscription
{
    public class CreateSubscription : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }
}
