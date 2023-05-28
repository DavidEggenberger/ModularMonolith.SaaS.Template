using Modules.TenantIdentityModule.Domain;
using Shared.Infrastructure.CQRS.Query;

namespace Shared.Modules.TenantIdentityModule.Application.Queries
{
    public class GetAllTenantMembershipsOfUser : IQuery<List<TenantMembership>>
    {
        public Guid UserId { get; set; }
    }
}
