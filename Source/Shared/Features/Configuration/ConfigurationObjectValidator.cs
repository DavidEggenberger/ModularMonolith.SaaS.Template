using Microsoft.Extensions.Options;

namespace Shared.Features.Configuration
{
    public abstract class ConfigurationObjectValidator<TConfiguration> : IValidateOptions<TConfiguration> where TConfiguration : ConfigurationObject
    {
        public abstract ValidateOptionsResult Validate(string name, TConfiguration options);
    }
}
