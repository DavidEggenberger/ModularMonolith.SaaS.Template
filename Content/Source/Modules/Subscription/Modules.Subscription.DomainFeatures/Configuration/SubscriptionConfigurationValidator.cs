using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Subscription.DomainFeatures.Configuration
{
    public class SubscriptionConfigurationValidator : IValidateOptions<SubscriptionConfiguration>
    {
        public ValidateOptionsResult Validate(string name, SubscriptionConfiguration options)
        {
            throw new NotImplementedException();
        }
    }
}
