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
            if (string.IsNullOrEmpty(efCoreConfiguration.DevelopmentSQLServerConnectionString))
            {
                return ValidateOptionsResult.Fail("");
            }

            if (string.IsNullOrEmpty(efCoreConfiguration.ProductionSQLServerConnectionString))
            {
                return ValidateOptionsResult.Fail("");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
