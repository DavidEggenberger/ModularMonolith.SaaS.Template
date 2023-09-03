using Modules.TenantIdentity.DomainFeatures.Aggregates.UserAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Shared.Infrastructure.CQRS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.DomainFeatures.Aggregates.UserAggregate.Application.Queries
{
    public class GetUserById : IQuery<User>
    {
        public Guid UserId { get; set; }
    }
    public class GetUserByIdHandler : IQueryHandler<GetUserById, User>
    {
        private readonly TenantIdentityDbContext tenantIdentityDbContext;
        public GetUserByIdHandler(TenantIdentityDbContext tenantIdentityDbContext)
        {
            this.tenantIdentityDbContext = tenantIdentityDbContext;
        }

        public async Task<User> HandleAsync(GetUserById query, CancellationToken cancellation)
        {
            return await tenantIdentityDbContext.GetUserByIdAsync(query.UserId);
        }
    }
}
