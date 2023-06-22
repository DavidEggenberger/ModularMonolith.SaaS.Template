using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.Configuration
{
    public class TenantIdentityConfiguration
    {
        public string StripeAPIKey { get; set; }
        public string StripeProfessionalPlanId { get; set; }
        public string StripeEnterprisePlanId { get; set; }
    }
}
