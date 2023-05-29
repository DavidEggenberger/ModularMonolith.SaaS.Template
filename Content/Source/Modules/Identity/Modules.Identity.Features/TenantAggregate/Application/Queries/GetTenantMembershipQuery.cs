using Modules.TenantIdentityModule.Infrastructure.EFCore;
using Shared.Features.Infrastructure.CQRS.Query;
using Modules.TenantIdentity.Features.TenantAggregate.Domain;
using System.Threading;

namespace Shared.Modules.TenantIdentityModule.Application.Queries 
{ 
    public class GetTenantMembershipQuery : IQuery<TenantMembership>
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
    public class GetTenantMembershipQueryHandler : BaseQueryHandler<TenantIdentityDbContext, Tenant>, IQueryHandler<GetTenantMembershipQuery, TenantMembership>
    {
        public GetTenantMembershipQueryHandler(TenantIdentityDbContext tenantDbContext) : base(tenantDbContext) { } 
        public async Task<TenantMembership> HandleAsync(GetTenantMembershipQuery query, CancellationToken cancellation)
        {
            return dbSet.First().Memberships.Single(m => m.UserId == query.UserId);
        }
    }
}
