using Shared.Infrastructure.CQRS.Query;
using System.Threading;
using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;

namespace Modules.TenantIdentity.DomainFeatures.Application.Queries
{ 
    public class GetTenantMembershipQuery : IQuery<TenantMembership>
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
    public class GetTenantMembershipQueryHandler : IQueryHandler<GetTenantMembershipQuery, TenantMembership>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        public GetTenantMembershipQueryHandler(TenantIdentityDbContext tenantDbContext)
        { 
            this.tenantIdentityDbContext = tenantDbContext;
        } 

        public async Task<TenantMembership> HandleAsync(GetTenantMembershipQuery query, CancellationToken cancellation)
        {
            return tenantIdentityDbContext.TenantMeberships.Single(m => m.UserId == query.UserId);
        }
    }
}
