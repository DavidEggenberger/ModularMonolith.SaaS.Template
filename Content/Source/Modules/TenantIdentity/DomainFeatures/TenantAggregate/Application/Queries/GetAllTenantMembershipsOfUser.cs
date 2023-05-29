using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;
using Shared.DomainFeatures.Infrastructure.CQRS.Query;

namespace Modules.TenantIdentity.DomainFeatures.Application.Queries
{
    public class GetAllTenantMembershipsOfUser : IQuery<List<TenantMembership>>
    {
        public Guid UserId { get; set; }
    }
}
