using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Application.Queries;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate.Application.Commands;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate.Application.Queries;
using Modules.TenantIdentity.Web.Shared.DTOs.Tenant;
using Modules.TenantIdentity.Web.Shared.DTOs.IdentityOperations;
using Shared.Features.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modules.TenantIdentity.Web.Server.Controllers.IdentityOperations
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class IdentityOperationsController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public IdentityOperationsController(SignInManager<ApplicationUser> signInManager, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<BFFUserInfoDTO> GetClaimsOfCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BFFUserInfoDTO.Anonymous;
            }
            return new BFFUserInfoDTO()
            {
                Claims = User.Claims.Select(claim => new ClaimValueDTO { Type = claim.Type, Value = claim.Value }).ToList()
            };
        }

        [HttpGet("selectTenant/{TenantId}")]
        public async Task<ActionResult> SetTenantForCurrentUser(Guid tenantId, [FromQuery] string redirectUri)
        {
            var user = await queryDispatcher.DispatchAsync<GetUserById, ApplicationUser>(new GetUserById { });

            var tenantMembershipsOfUserQuery = new GetAllTenantMembershipsOfUser() { UserId = user.Id };
            var tenantMemberships = await queryDispatcher.DispatchAsync<GetAllTenantMembershipsOfUser, List<TenantMembershipDTO>>(tenantMembershipsOfUserQuery);

            if (tenantMemberships.Select(t => t.TenantId).Contains(tenantId))
            {
                var setSelectedTenantForUser = new SetSelectedTenantForUser { };
                await commandDispatcher.DispatchAsync(setSelectedTenantForUser);
                await signInManager.RefreshSignInAsync(user);
            }
            else
            {
                throw new Exception();
            }

            return LocalRedirect(redirectUri ?? "/");
        }

        [HttpGet("Logout")]
        public async Task<ActionResult> LogoutCurrentUser([FromQuery] string redirectUri)
        {
            await signInManager.SignOutAsync();
            return LocalRedirect(redirectUri ?? "/");
        }
    }
}
