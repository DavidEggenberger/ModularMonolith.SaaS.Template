using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.Infrastructure.Configuration
{
    public class SubscriptionConfiguration
    {
        public string StripeProfessionalPlanId { get; set; }
        public string StripeEnterprisePlanId { get; set; }
        public const string StripeAPIKeyConstant = "Subscription:StripeAPIKey";
    }
}
