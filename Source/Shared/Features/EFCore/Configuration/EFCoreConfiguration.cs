using Shared.Features.Configuration;

namespace Shared.Features.EFCore.Configuration
{
    public class EFCoreConfiguration : ConfigurationObject
    {
        public string SQLServerConnectionString_Dev { get; set; }
        public string SQLServerConnectionString_Prod { get; set; }
    }
}
