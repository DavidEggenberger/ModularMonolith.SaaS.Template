using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.EFCore.Configuration
{
    internal class EFCoreConfigurationValidator : IValidateOptions<EFCoreConfiguration>
    {
        public ValidateOptionsResult Validate(string name, EFCoreConfiguration efCoreConfiguration)
        {
            if (string.IsNullOrEmpty(efCoreConfiguration.SQLServerConnectionString))
            {
                return ValidateOptionsResult.Fail("");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
