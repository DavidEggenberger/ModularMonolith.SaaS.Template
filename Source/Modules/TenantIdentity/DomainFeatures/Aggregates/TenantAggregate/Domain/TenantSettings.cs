using Shared.DomainFeatures.DomainKernel;

namespace Modules.TenantIdentity.DomainFeatures.Aggregates.TenantAggregate.Domain
{
    public class TenantSettings : Entity
    {
        public string IconURI { get; set; }
    }
}
