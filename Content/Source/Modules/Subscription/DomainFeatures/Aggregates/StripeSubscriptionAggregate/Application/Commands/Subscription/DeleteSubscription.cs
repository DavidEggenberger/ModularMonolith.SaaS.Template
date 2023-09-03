using Shared.Infrastructure.CQRS.Command;
using Stripe;

namespace Modules.Subscription.DomainFeatures.Aggregates.StripeSubscriptionAggregate.Application.Commands.Subscription
{
    public class DeleteSubscription : ICommand
    {
        public Stripe.Subscription Subscription { get; set; }
    }
}
