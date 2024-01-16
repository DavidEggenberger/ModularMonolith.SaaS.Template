using Shared.Features.DomainKernel;

namespace Modules.TenantIdentity.Features.Domain.TenantAggregate
{
    public class TenantSettings : Entity
    {
        public string IconURI { get; set; }
    }
}
