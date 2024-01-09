using Shared.Features.CQRS.Query;
using System.Threading;
using Modules.TenantIdentity.Features.Infrastructure.EFCore;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant;

namespace Modules.TenantIdentity.Features.Application.Queries.TenantAggregate
{
    public class GetTenantMembershipQuery : IQuery<TenantMembershipDTO>
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
    public class GetTenantMembershipQueryHandler : IQueryHandler<GetTenantMembershipQuery, TenantMembershipDTO>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        public GetTenantMembershipQueryHandler(TenantIdentityDbContext tenantDbContext)
        {
            tenantIdentityDbContext = tenantDbContext;
        }

        public async Task<TenantMembershipDTO> HandleAsync(GetTenantMembershipQuery query, CancellationToken cancellation)
        {
            var tenantMembership = tenantIdentityDbContext.TenantMeberships.Single(m => m.UserId == query.UserId);
            return tenantMembership.ToDTO();
        }
    }
}
