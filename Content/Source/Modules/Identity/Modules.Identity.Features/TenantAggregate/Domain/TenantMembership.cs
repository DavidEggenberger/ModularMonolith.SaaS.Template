using Domain.Aggregates.TenantAggregate.Enums;
using Shared.Domain;

namespace Modules.TetnantIdentity.Features.TenantAggregate.Domain
{
    public class TenantMembership : Entity
    {
        private TenantMembership() { }
        public TenantMembership(Guid userId, Role role)
        {
            UserId = userId;
            Role = role;
        }
        public Guid UserId { get; set; }
        public Tenant Tenant { get; set; }
        public Role Role { get; set; }
    }
}
