using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.Infrastructure.Configuration
{
    public class TenantIdentityConfigurationValidator : IValidateOptions<TenantIdentityConfiguration>
    {
        public ValidateOptionsResult Validate(string name, TenantIdentityConfiguration tenantIdentityConfiguration)
        {
            if (string.IsNullOrEmpty(tenantIdentityConfiguration.StripeAPIKey))
            {
                return ValidateOptionsResult.Fail("");
            }

            if (string.IsNullOrEmpty(tenantIdentityConfiguration.StripeEnterprisePlanId))
            {
                return ValidateOptionsResult.Fail("");
            }

            if (string.IsNullOrEmpty(tenantIdentityConfiguration.StripeProfessionalPlanId))
            {
                return ValidateOptionsResult.Fail("");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
