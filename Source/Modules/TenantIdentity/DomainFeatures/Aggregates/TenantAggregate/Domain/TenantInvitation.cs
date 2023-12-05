using Shared.Infrastructure.DomainKernel;
using Shared.Kernel.BuildingBlocks.Authorization.Roles;

namespace Modules.TenantIdentity.DomainFeatures.Aggregates.TenantAggregate.Domain
{
    public class TenantInvitation : ValueObject
    {
        public Guid UserId { get; set; }
        public TenantRole Role { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return Role;
        }
    }
}
