using Microsoft.Extensions.Options;

namespace Shared.Features.EFCore.Configuration
{
    internal class EFCoreConfigurationValidator : IValidateOptions<EFCoreConfiguration>
    {
        public ValidateOptionsResult Validate(string name, EFCoreConfiguration efCoreConfiguration)
        {
            if (string.IsNullOrEmpty(efCoreConfiguration.SQLServerConnectionString_Dev))
            {
                return ValidateOptionsResult.Fail("");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
