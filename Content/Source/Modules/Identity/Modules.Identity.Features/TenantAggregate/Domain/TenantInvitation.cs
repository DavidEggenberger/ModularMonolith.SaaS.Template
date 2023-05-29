using Domain.Aggregates.TenantAggregate.Enums;
using Shared.Domain;
using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.TenantIdentity.Features.TenantAggregate.Domain
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
