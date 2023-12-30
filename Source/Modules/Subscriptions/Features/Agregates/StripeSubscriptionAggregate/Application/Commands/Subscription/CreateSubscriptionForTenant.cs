using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate.Application.Commands.Subscription
{
    public class CreateSubscriptionForTenant : ICommand
    {
        public Guid TenantId { get; set; }
        public Stripe.Subscription Subscription { get; set; }
    }
}
