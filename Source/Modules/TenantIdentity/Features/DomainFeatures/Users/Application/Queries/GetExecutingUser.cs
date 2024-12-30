using Shared.Features.Messaging.Queries;
using Shared.Features.Server;
using System.Threading;

namespace Modules.TenantIdentity.Features.DomainFeatures.Users.Application.Queries
{
    public class GetExecutingUser : Query<ApplicationUser>
    {
    }
    public class GetUserByIdHandler : ServerExecutionBase<TenantIdentityModule>, IQueryHandler<GetExecutingUser, ApplicationUser>
    {
        public GetUserByIdHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<ApplicationUser> HandleAsync(GetExecutingUser query, CancellationToken cancellation)
        {
            return await module.TenantIdentityDbContext.GetUserByIdAsync(query.ExecutingUserId);
        }
    }
}
