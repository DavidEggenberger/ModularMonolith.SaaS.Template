using Shared.Features.Domain;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain
{
    public class TenantConfiguration : Entity
    {
        public string IconURI { get; set; }
    }
}
