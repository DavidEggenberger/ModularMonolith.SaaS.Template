using Shared.Infrastructure.DomainKernel;
using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain
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
