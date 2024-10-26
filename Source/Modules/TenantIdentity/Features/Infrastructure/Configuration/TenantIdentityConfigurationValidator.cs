using Microsoft.Extensions.Options;
using Shared.Features.Configuration;

namespace Modules.TenantIdentity.Features.Infrastructure.Configuration
{
    public class TenantIdentityConfigurationValidator : ConfigurationObjectValidator<TenantIdentityConfiguration>
    {
        public override ValidateOptionsResult Validate(string name, TenantIdentityConfiguration tenantIdentityConfiguration)
        {
            if (string.IsNullOrEmpty(tenantIdentityConfiguration.GoogleClientId))
            {
                return ValidateOptionsResult.Fail("");
            }

            if (string.IsNullOrEmpty(tenantIdentityConfiguration.MicrosoftClientId))
            {
                return ValidateOptionsResult.Fail("");
            }

            if (string.IsNullOrEmpty(tenantIdentityConfiguration.GoogleClientSecret))
            {
                return ValidateOptionsResult.Fail("");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
