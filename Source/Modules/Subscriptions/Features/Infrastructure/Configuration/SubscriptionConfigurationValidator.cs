using Microsoft.Extensions.Options;

namespace Modules.Subscription.Features.Infrastructure.Configuration
{
    public class SubscriptionConfigurationValidator : IValidateOptions<SubscriptionConfiguration>
    {
        public ValidateOptionsResult Validate(string name, SubscriptionConfiguration options)
        {
            if (string.IsNullOrEmpty(options.StripeProfessionalPlanPriceId))
            {
                throw new ArgumentNullException();
            }

            return ValidateOptionsResult.Success;
        }
    }
}
