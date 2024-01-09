using Microsoft.EntityFrameworkCore;
using Modules.TenantIdentity.Features.Infrastructure.EFCore;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant;
using Shared.Features.CQRS.Query;
using System.Threading;

namespace Modules.TenantIdentity.Features.Application.Queries.TenantAggregate
{
    public class GetTenantDetailsByID : IQuery<TenantDetailDTO>
    {
        public Guid TenantId { get; set; }
    }
    public class GetTenantDetailsByIDQueryHandler : IQueryHandler<GetTenantDetailsByID, TenantDetailDTO>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        public GetTenantDetailsByIDQueryHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }

        public async Task<TenantDetailDTO> HandleAsync(GetTenantDetailsByID query, CancellationToken cancellation)
        {
            var tenantDetail = await tenantIdentityDbContext.Tenants.Where(t => t.TenantId == query.TenantId).SingleAsync();
            return tenantDetail.ToDetailDTO();
        }
    }
}
