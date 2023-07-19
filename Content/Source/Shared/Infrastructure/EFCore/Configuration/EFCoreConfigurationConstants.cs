using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.EFCore.Configuration
{
    public class EFCoreConfigurationConstants
    {
        public const string DevelopmentSQLServerConnectionString = nameof(DevelopmentSQLServerConnectionString);
        public const string ProductionSQLServerConnectionString = nameof(ProductionSQLServerConnectionString);
    }
}
