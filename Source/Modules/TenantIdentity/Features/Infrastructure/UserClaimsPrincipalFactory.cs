using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Modules.TenantIdentity.Features.DomainFeatures.Users;
using Modules.TenantIdentity.Features.DomainFeatures.Users.Application.Queries;
using Shared.Features.Misc;
using Shared.Features.Misc.ExecutionContext;
using Shared.Kernel.Constants.Auth;

namespace Modules.TenantIdentity.Features.Infrastructure
{
    public class ContextUserClaimsPrincipalFactory<TUser> : ServerExecutionBase<TenantIdentityModule>, IUserClaimsPrincipalFactory<TUser> where TUser : ApplicationUser
    {
        public ContextUserClaimsPrincipalFactory(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            var claimsForUser = await queryDispatcher.DispatchAsync<GetClaimsForExecutingUser, IEnumerable<Claim>>(new GetClaimsForExecutingUser { });
            
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
