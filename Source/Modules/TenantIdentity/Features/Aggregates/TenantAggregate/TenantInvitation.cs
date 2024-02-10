using Modules.TenantIdentity.Features.Aggregates.UserAggregate;
using Shared.Features.Domain;
using Shared.Kernel.BuildingBlocks.Auth;

namespace Modules.TenantIdentity.Features.Domain.TenantAggregate
{
    public class TenantInvitation : ValueObject
    {
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public TenantRole Role { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return Role;
        }
    }
}
