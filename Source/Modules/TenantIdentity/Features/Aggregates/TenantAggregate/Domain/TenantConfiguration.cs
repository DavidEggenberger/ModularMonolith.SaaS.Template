using Shared.Features.Domain;

namespace Modules.TenantIdentity.Features.Aggregates.TenantAggregate.Domain
{
    public class TenantConfiguration : Entity
    {
        public string IconURI { get; set; }
    }
}
