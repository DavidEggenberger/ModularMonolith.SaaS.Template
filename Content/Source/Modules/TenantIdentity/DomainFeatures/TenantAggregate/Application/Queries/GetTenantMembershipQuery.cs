using Shared.Infrastructure.CQRS.Query;
using System.Threading;
using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant;

namespace Modules.TenantIdentity.DomainFeatures.Application.Queries
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
            this.tenantIdentityDbContext = tenantDbContext;
        } 

        public async Task<TenantMembershipDTO> HandleAsync(GetTenantMembershipQuery query, CancellationToken cancellation)
        {
            var tenantMembership = tenantIdentityDbContext.TenantMeberships.Single(m => m.UserId == query.UserId);
            return tenantMembership.ToDTO();
        }
    }
}
