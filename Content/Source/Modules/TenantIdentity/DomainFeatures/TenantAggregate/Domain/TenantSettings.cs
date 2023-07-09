using Shared.Infrastructure.DomainKernel;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain
{
    public class TenantSettings : Entity
    {
        public string IconURI { get; set; }
    }
}
