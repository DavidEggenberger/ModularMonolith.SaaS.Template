using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Kernel.Constants.Modules
{
    public partial class EndpointConstants
    {
        public static class Subscriptions
        {
            public const string StripePremiumSubscriptionPath = "/api/stripe/subscribe/premium";
            public const string StripeEnterpriseSubscriptionPath = "/api/stripe/subscribe/enterprise";
        }    
    }
}
