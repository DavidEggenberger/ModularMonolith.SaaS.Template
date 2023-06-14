using Shared.Infrastructure.CQRS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.Application.Commands
{
    public class CreateStripeBillingPortalSession : ICommand<Stripe.BillingPortal.Session>
    {
    }
}
