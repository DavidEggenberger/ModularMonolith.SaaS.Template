using Shared.Infrastructure.CQRS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.Application.Queries
{
    public class GetStripeCheckoutSession : IQuery<Stripe.Checkout.Session>
    {
        public string SessionId { get; set; }
    }
}
