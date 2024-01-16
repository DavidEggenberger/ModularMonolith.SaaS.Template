using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.Domain.StripeSubscriptionAggregate.Commands
{
    public class CreateSubscriptionForTenant : ICommand
    {
        public Guid TenantId { get; set; }
        public Stripe.Subscription Subscription { get; set; }
    }
}
