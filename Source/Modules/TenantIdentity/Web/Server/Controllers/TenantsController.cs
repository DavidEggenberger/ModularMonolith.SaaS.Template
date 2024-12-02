using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Kernel.BuildingBlocks.Auth.Attributes;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Shared.Kernel.BuildingBlocks.Auth.Constants;
using Modules.TenantIdentity.Features.DomainFeatures.Users;
using Shared.Features.Server;
using Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Queries;
using Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Commands;
using Modules.TenantIdentity.Features.DomainFeatures.Users.Application.Queries;
using Modules.TenantIdentity.Shared.DTOs.Tenant;
using Modules.TenantIdentity.Shared.DTOs.Tenant.Operations;

namespace Modules.TenantIdentity.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = AuthConstant.ApplicationAuthenticationScheme)]
    [ApiController]
    public class TenantsController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public TenantsController(SignInManager<ApplicationUser> signInManager, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.signInManager = signInManager;
        }

        [HttpGet("{tenantId}")]
        [AuthorizeTenantAdmin]
        public async Task<ActionResult<TenantDTO>> GetTenant([FromRoute] Guid tenantId)
        {
            var tenant = await queryDispatcher.DispatchAsync<GetTenantByID, TenantDTO>(new GetTenantByID { TenantId = tenantId });

            return Ok(tenant);
        }

        [HttpGet("{tenantId}/details")]
        public async Task<ActionResult<TenantDetailDTO>> GetTenantDetail([FromRoute] Guid tenantId)
        {
            TenantDetailDTO tenantDetail = await queryDispatcher.DispatchAsync<GetTenantDetailsByID, TenantDetailDTO>(new GetTenantDetailsByID { TenantId = tenantId });

            return Ok(tenantDetail);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TenantDTO>>> GetTenantMemberships()
        {
            var getTenantMemberships = new GetAllTenantMembershipsOfUser { ExecutingUserId = executionContext.UserId };
            List<TenantMembershipDTO> tenantMemberships = await queryDispatcher.DispatchAsync<GetAllTenantMembershipsOfUser, List<TenantMembershipDTO>>(getTenantMemberships);
            
            return Ok(tenantMemberships);
        }

        [HttpPost]
        public async Task<ActionResult<TenantDTO>> CreateTenant(CreateTenantDTO createTenantDTO)
        {
            validationService.ThrowIfInvalidModel(createTenantDTO);

            var createTenant = new CreateTenantWithAdmin
            {
                AdminId = executionContext.UserId,
                Name = createTenantDTO.Name
            };
            var createdTenant = await commandDispatcher.DispatchAsync<CreateTenantWithAdmin, TenantDTO>(createTenant);

            var user = await queryDispatcher.DispatchAsync<GetUserById, ApplicationUser>(new GetUserById { ExecutingUserId = executionContext.UserId });
            await signInManager.RefreshSignInAsync(user);
            
            return CreatedAtAction(nameof(CreateTenant), createdTenant);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTenant([FromRoute] Guid id)
        {
            await commandDispatcher.DispatchAsync<DeleteTenant>(new DeleteTenant { ExecutingUserId = executionContext.UserId, TenantId = id });

            return Ok();
        }

        [HttpPost("memberships")]
        public async Task<ActionResult> CreateTenantMembership(InviteUserToTenantDTO inviteUserToGroupDTO)
        {
            

            await commandDispatcher.DispatchAsync<AddUserToTenant>(null);
            
            return Ok();
        }

        [HttpPut("memberships")]
        public async Task<ActionResult> UpdateTenantMembership(ChangeRoleOfTenantMemberDTO changeRoleOfTeamMemberDTO)
        {
            await commandDispatcher.DispatchAsync<UpdateTenantMembership>(new UpdateTenantMembership { });

            return Ok();
        }

        [HttpDelete("memberships/{userId}")]
        public async Task<ActionResult> DeleteTenantMembership(Guid id)
        {
            await commandDispatcher.DispatchAsync<RemoveUserFromTenant>(new RemoveUserFromTenant { });

            return Ok();
        }
    }
}

