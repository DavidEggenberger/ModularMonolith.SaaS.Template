using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Kernel.BuildingBlocks.Auth.Attributes;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Modules.TenantIdentity.Web.Shared.DTOs.Tenant;
using Modules.TenantIdentity.Web.Shared.DTOs.Tenant.Operations;
using Shared.Kernel.BuildingBlocks.Auth.Constants;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate;
using Shared.Features.Server;
using Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Application.Queries;
using Modules.TenantIdentity.Features.DomainFeatures.TenantAggregate.Application.Commands;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate.Application.Queries;

namespace Modules.TenantIdentity.Web.Server.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<TenantDTO>> GetTenant()
        {
            var tenantId = executionContext.TenantId;
            TenantDTO tenant = await queryDispatcher.DispatchAsync<GetTenantByID, TenantDTO>(new GetTenantByID { TenantId = tenantId });

            return Ok(tenant);
        }

        [HttpGet("{tenantId}/details")]
        public async Task<ActionResult<TenantDetailDTO>> GetTenantDetail()
        {
            var tenantId = executionContext.TenantId;
            TenantDetailDTO tenantDetail = await queryDispatcher.DispatchAsync<GetTenantDetailsByID, TenantDetailDTO>(new GetTenantDetailsByID { TenantId = tenantId });

            return Ok(tenantDetail);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = AuthConstant.ApplicationAuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<TenantDTO>>> GetAllTenantsWhereUserIsMember()
        {
            var userId = executionContext.UserId;
            List<TenantMembershipDTO> teamMemberships = await queryDispatcher.DispatchAsync<GetAllTenantMembershipsOfUser, List<TenantMembershipDTO>>(null);
            
            return Ok(teamMemberships);
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
            var createdTenant = await commandDispatcher.DispatchAsync<CreateTenantWithAdmin, TenantDTO>(null);

            var user = await queryDispatcher.DispatchAsync<GetUserById, ApplicationUser>(new GetUserById { });
            await signInManager.RefreshSignInAsync(user);
            
            return CreatedAtAction(nameof(CreateTenant), createdTenant);
        }

        [HttpDelete("{id}")]
        public async Task DeleteTenant(Guid id)
        {
            await commandDispatcher.DispatchAsync<DeleteTenant>(new DeleteTenant { });

            var userId = executionContext.UserId;
            var user = await queryDispatcher.DispatchAsync<GetUserById, ApplicationUser>(new GetUserById { });

            await signInManager.RefreshSignInAsync(user);
        }

        [HttpPost("memberships")]
        public async Task<ActionResult> CreateTenantMembership(InviteUserToTenantDTO inviteUserToGroupDTO)
        {
            var userId = executionContext.UserId;

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

