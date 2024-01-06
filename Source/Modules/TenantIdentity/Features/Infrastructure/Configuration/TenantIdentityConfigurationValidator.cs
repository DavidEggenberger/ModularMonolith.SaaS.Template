using Microsoft.Extensions.Options;

namespace Modules.TenantIdentity.Features.Infrastructure.Configuration
{
    public class TenantIdentityConfigurationValidator : IValidateOptions<TenantIdentityConfiguration>
    {
        public ValidateOptionsResult Validate(string name, TenantIdentityConfiguration tenantIdentityConfiguration)
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
