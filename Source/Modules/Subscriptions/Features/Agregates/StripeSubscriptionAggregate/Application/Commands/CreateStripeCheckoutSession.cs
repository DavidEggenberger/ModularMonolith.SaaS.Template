using Shared.Features.CQRS.Command;
using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate.Application.Commands
{
    public class CreateStripeCheckoutSession : ICommand<Stripe.Checkout.Session>
    {
        public SubscriptionPlanType SubscriptionPlanType { get; set; }
        public Guid TenantId { get; set; }
        public string RedirectBaseUrl { get; set; }
        public string StripeCustomerId { get; set; }
    }
}
