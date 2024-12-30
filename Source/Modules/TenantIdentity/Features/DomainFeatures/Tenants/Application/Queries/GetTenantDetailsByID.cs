using Microsoft.EntityFrameworkCore;
using Modules.TenantIdentity.Public.DTOs.Tenant;
using Shared.Features.Messaging.Queries;
using Shared.Features.Server;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Queries
{
    public class GetTenantDetailsByID : Query<TenantExtendedDTO>
    {
        public Guid TenantId { get; set; }
    }
    public class GetTenantDetailsByIDQueryHandler : ServerExecutionBase<TenantIdentityModule>, IQueryHandler<GetTenantDetailsByID, TenantExtendedDTO>
    {
        public GetTenantDetailsByIDQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<TenantExtendedDTO> HandleAsync(GetTenantDetailsByID query, CancellationToken cancellation)
        {
            var tenantDetail = await module.TenantIdentityDbContext.Tenants.Where(t => t.TenantId == query.TenantId).SingleAsync();
            return tenantDetail.ToDetailDTO();
        }
    }
}
