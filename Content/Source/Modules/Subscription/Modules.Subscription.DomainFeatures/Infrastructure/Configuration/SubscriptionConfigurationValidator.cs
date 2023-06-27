using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.Infrastructure.Configuration
{
    public class SubscriptionConfigurationValidator : IValidateOptions<SubscriptionConfiguration>
    {
        public ValidateOptionsResult Validate(string name, SubscriptionConfiguration options)
        {
            if (string.IsNullOrEmpty(options.StripeProfessionalPlanId))
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(options.StripeProfessionalPlanId))
            {
                throw new ArgumentNullException();
            }

            return ValidateOptionsResult.Success;
        }
    }
}
