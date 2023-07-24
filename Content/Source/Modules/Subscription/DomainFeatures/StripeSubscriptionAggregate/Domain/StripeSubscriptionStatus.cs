using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.StripeSubscriptionAggregate.Domain
{
    public enum StripeSubscriptionStatus
    {
        Active,
        Canceled,
        Trialing,
        Unpaid
    }
}
