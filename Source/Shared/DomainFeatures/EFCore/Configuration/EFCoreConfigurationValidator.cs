﻿using Microsoft.Extensions.Options;

namespace Shared.DomainFeatures.EFCore.Configuration
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