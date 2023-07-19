using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.EFCore.Configuration
{
    public class EFCoreConfiguration
    {
        public string DevelopmentSQLServerConnectionString { get; set; }
        public string ProductionSQLServerConnectionString { get; set; }
    }
}
