using Shared.Infrastructure.CQRS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.StripeSubscriptionAggregate.Application.Commands
{
    public class CreateStripeBillingPortalSession : ICommand<Stripe.BillingPortal.Session>
    {
        public string StripeCustomerId { get; set; }
        public string RedirectBaseUrl { get; set; }
    }
}
