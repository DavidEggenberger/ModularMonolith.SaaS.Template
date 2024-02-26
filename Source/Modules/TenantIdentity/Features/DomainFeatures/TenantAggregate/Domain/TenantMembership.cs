using Modules.TenantIdentity.Web.Shared.DTOs.Tenant;
using Shared.Features.Domain;
using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Domain
{
    public class TenantMembership : Entity
    {
        private TenantMembership() { }
        public TenantMembership(Guid userId, TenantRole role)
        {
            UserId = userId;
            Role = role;
        }

        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public TenantRole Role { get; set; }

        public TenantMembershipDTO ToDTO()
        {
            return new TenantMembershipDTO()
            {
                UserId = UserId,
                TenantId = Tenant.Id,
                Role = Role
            };
        }
    }
}
