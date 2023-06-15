using Shared.Infrastructure.CQRS.Command;
using Shared.Kernel.BuildingBlocks.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.Application.Commands
{
    public class CreateStripeCheckoutSession : ICommand<Stripe.Checkout.Session>
    {
        public SubscriptionPlanType SubscriptionPlanType { get; set; }
        public Guid TenantId { get; set; }
        public string RedirectBaseUrl { get; set; }
        public string StripeCustomerId { get; set; }
    }
}
