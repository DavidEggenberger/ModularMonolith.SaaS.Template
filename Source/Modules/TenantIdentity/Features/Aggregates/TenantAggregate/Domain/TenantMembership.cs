using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant;
using Shared.Features.DomainKernel;
using Shared.Kernel.BuildingBlocks.Auth.Roles;

namespace Modules.TenantIdentity.Features.Aggregates.TenantAggregate.Domain
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
        public Tenant Tenant { get; set; }
        public TenantRole Role { get; set; }

        public TenantMembershipDTO ToDTO()
        {
            return new TenantMembershipDTO();
        }
    }
}
