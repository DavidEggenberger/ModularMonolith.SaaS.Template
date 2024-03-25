using Shared.Features.Domain;
using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Domain
{
    public class TenantInvitation : Entity
    {
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public string Email { get; set; }
        public TenantRole Role { get; set; }
    }
}
