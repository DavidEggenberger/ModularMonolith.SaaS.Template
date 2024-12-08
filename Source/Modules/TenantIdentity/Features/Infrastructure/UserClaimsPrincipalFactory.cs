using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Shared.Kernel.BuildingBlocks.Auth.Constants;
using Modules.TenantIdentity.Features.DomainFeatures.Users;
using Modules.TenantIdentity.Features.DomainFeatures.Users.Application.Queries;
using Shared.Features.Server;

namespace Modules.TenantIdentity.Features.Infrastructure
{
    public class ContextUserClaimsPrincipalFactory<TUser> : ServerExecutionBase<TenantIdentityModule>, IUserClaimsPrincipalFactory<TUser> where TUser : ApplicationUser
    {
        public ContextUserClaimsPrincipalFactory(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var claimsForUser = await queryDispatcher.DispatchAsync<GetClaimsForExecutingUser, IEnumerable<Claim>>(new GetClaimsForExecutingUser { ExecutingUserId = user.Id });
            
            var claimsIdentity = new ClaimsIdentity(
                claims: claimsForUser, 
                authenticationType: IdentityConstants.ApplicationScheme, 
                nameType: ClaimConstants.UserNameClaimType, 
                roleType: ClaimConstants.UserRoleInTenantClaimType);

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            return claimsPrincipal;
        }
    }
}
