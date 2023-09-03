using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.Aggregates.StripeSubscriptionAggregate.Domain
{
    public class StripeCustomer
    {
        public Guid UserId { get; set; }
        public string StripeCustomerId { get; set; }
    }
}
