using Modules.TenantIdentity.DomainFeatures.Aggregates.UserAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Shared.DomainFeatures.CQRS.Query;
using System.Threading;

namespace Modules.TenantIdentity.DomainFeatures.Aggregates.UserAggregate.Application.Queries
{
    public class GetUserById : IQuery<ApplicationUser>
    {
        public Guid UserId { get; set; }
    }
    public class GetUserByIdHandler : IQueryHandler<GetUserById, ApplicationUser>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        public GetUserByIdHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }

        public async Task<ApplicationUser> HandleAsync(GetUserById query, CancellationToken cancellation)
        {
            return await tenantIdentityDbContext.GetUserByIdAsync(query.UserId);
        }
    }
}
