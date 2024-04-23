using Microsoft.Extensions.Options;

namespace Modules.Subscriptions.Features.Infrastructure.Configuration
{
    public class SubscriptionsConfigurationValidator : IValidateOptions<SubscriptionsConfiguration>
    {
        public ValidateOptionsResult Validate(string name, SubscriptionsConfiguration options)
        {
            if (string.IsNullOrEmpty(options.StripeProfessionalPlanPriceId))
            {
                throw new ArgumentNullException();
            }

            return ValidateOptionsResult.Success;
        }
    }
}
