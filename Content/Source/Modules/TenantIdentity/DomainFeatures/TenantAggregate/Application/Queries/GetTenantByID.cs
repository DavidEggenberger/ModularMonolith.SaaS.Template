using Microsoft.EntityFrameworkCore;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant;
using Shared.Infrastructure.CQRS.Query;
using System.Threading;

namespace Modules.TenantIdentity.DomainFeatures.Application.Queries
{
    public class GetTenantByID : IQuery<TenantDTO>
    {
        public Guid TenantId { get; set; }
    }
    public class GetTenantByIdQueryHandler : IQueryHandler<GetTenantByID, TenantDTO>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        public GetTenantByIdQueryHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }

        public async Task<TenantDTO> HandleAsync(GetTenantByID query, CancellationToken cancellation)
        {
            var tenant = await tenantIdentityDbContext.GetTenantByIdAsync(query.TenantId);
            return tenant.ToDTO();
        }
    }
}
