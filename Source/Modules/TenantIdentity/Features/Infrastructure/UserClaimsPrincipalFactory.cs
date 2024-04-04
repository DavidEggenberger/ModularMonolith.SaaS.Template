using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Shared.Features.Messaging.Query;
using Shared.Kernel.BuildingBlocks.Auth.Constants;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate.Application.Queries;

namespace Modules.TenantIdentity.Features.Infrastructure
{
    public class ContextUserClaimsPrincipalFactory<TUser> : IUserClaimsPrincipalFactory<TUser> where TUser : ApplicationUser
    {
        private readonly IQueryDispatcher queryDispatcher;
        public ContextUserClaimsPrincipalFactory(IQueryDispatcher queryDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
        }
        public async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var claimsForUserQuery = new GetClaimsForUser { UserId = user.Id };
            var claimsForUser = await queryDispatcher.DispatchAsync<GetClaimsForUser, IEnumerable<Claim>>(claimsForUserQuery);
            
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimsForUser, IdentityConstants.ApplicationScheme, nameType: ClaimConstants.UserNameClaimType, ClaimConstants.UserRoleInTenantClaimType);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            return claimsPrincipal;
        }
    }
}
