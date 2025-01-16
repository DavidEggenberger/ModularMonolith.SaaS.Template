using Microsoft.Extensions.Options;
using Shared.Features.Misc.Configuration;

namespace Modules.Subscriptions.Features.Infrastructure.Configuration
{
    public class SubscriptionsConfigurationValidator : ConfigurationObjectValidator<SubscriptionsConfiguration>
    {
        public override ValidateOptionsResult Validate(string name, SubscriptionsConfiguration options)
        {
            if (string.IsNullOrEmpty(options.StripeProfessionalPlanPriceId))
            {
                throw new ArgumentNullException();
            }

            return ValidateOptionsResult.Success;
        }
    }
}
