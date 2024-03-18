using Shared.Features.Modules.Configuration;

namespace Shared.Features.EFCore.Configuration
{
    public class EFCoreConfiguration : IModuleConfiguration
    {
        public string SQLServerConnectionString_Dev { get; set; }
        public string SQLServerConnectionString_Prod { get; set; }
    }
}
