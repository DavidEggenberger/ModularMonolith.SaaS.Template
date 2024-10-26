using Shared.Features.Configuration;

namespace Modules.TenantIdentity.Features.Infrastructure.Configuration
{
    public class TenantIdentityConfiguration : ConfigurationObject
    {
        public string GoogleClientId { get; set; }
        public string GoogleClientSecret { get; set; }
        public string MicrosoftClientId { get; set; }
        public string MicrosoftClientSecret { get; set; }
        public string LinkedinClientId { get; set; }
        public string LinkedinClientSecret { get; set; }
    }
}
