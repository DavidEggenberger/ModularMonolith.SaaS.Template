using Domain.Aggregates.TenantAggregate.Enums;
using Shared.Domain;
using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.TenantIdentity.Features.TenantAggregate.Domain
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
    }
}
