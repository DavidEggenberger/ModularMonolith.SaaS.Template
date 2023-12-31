using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate.Application.Commands
{
    public class CreateSubscriptionForTenant : ICommand
    {
        public Guid TenantId { get; set; }
        public Stripe.Subscription Subscription { get; set; }
    }
}
