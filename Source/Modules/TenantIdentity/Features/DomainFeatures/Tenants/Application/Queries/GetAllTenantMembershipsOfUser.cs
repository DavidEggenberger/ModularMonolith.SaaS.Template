using Microsoft.EntityFrameworkCore;
using Modules.TenantIdentity.Public.DTOs.Tenant;
using Shared.Features.Messaging.Queries;
using Shared.Features.Misc.ExecutionContext;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Queries
{
    public class GetAllTenantMembershipsOfUser : Query<List<TenantMembershipDTO>> { }

    public class GetAllTenantMembershipsOfUserQueryHandler : ServerExecutionBase<TenantIdentityModule>, IQueryHandler<GetAllTenantMembershipsOfUser, List<TenantMembershipDTO>>
    {
        public GetAllTenantMembershipsOfUserQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider) { } 

        public async Task<List<TenantMembershipDTO>> HandleAsync(GetAllTenantMembershipsOfUser query, CancellationToken cancellation)
        {
            var tenantMemberships = await module.TenantIdentityDbContext.TenantMemberships.Where(tm => tm.UserId == executionContext.UserId).ToListAsync();
            return tenantMemberships.Select(tm => tm.ToDTO()).ToList();
        }
    }
}
