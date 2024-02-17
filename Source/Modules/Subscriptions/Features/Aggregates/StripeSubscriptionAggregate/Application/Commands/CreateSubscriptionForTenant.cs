using Shared.Features.CQRS.Command;

namespace Modules.Subscriptions.Features.Aggregates.StripeSubscriptionAggregate.Application.Commands
{
    public class CreateSubscriptionForTenant : ICommand
    {
        public Guid TenantId { get; set; }
        public Stripe.Subscription Subscription { get; set; }
    }
}
