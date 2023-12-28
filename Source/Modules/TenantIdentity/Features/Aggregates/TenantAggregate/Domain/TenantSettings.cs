using Shared.Features.DomainKernel;

namespace Modules.TenantIdentity.Features.Aggregates.TenantAggregate.Domain
{
    public class TenantSettings : Entity
    {
        public string IconURI { get; set; }
    }
}
