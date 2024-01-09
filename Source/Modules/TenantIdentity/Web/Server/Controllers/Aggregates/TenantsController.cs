using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Kernel.BuildingBlocks.Auth.Attributes;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared.Features;
using System;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant.Operations;
using Shared.Kernel.BuildingBlocks.Auth.Constants;
using Modules.TenantIdentity.Features.Aggregates.UserAggregate;
using Modules.TenantIdentity.Features.Application.Queries.UserAggregate;
using Modules.TenantIdentity.Features.Application.Queries.TenantAggregate;
using Modules.TenantIdentity.Features.Application.Commands.TenantAggregate;

namespace Modules.TenantIdentity.Web.Server.Controllers.Aggregates
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
            var tenantId = executionContextAccessor.TenantId;
            TenantDTO tenant = await queryDispatcher.DispatchAsync<GetTenantByID, TenantDTO>(new GetTenantByID { TenantId = tenantId });

            return Ok(tenant);
        }

        [HttpGet("{tenantId}/details")]
        public async Task<ActionResult<TenantDetailDTO>> GetTenantDetail()
        {
            var tenantId = executionContextAccessor.TenantId;
            TenantDetailDTO tenantDetail = await queryDispatcher.DispatchAsync<GetTenantDetailsByID, TenantDetailDTO>(new GetTenantDetailsByID { TenantId = tenantId });

            return Ok(tenantDetail);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = AuthConstant.ApplicationAuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<TenantDTO>>> GetAllTenantsWhereUserIsMember()
        {
            var userId = executionContextAccessor.UserId;
            List<TenantMembershipDTO> teamMemberships = await queryDispatcher.DispatchAsync<GetAllTenantMembershipsOfUser, List<TenantMembershipDTO>>(null);
            
            return Ok(teamMemberships);
        }

        [HttpPost]
        public async Task<ActionResult<TenantDTO>> CreateTenant(CreateTenantDTO createTenantDTO)
        {
            validationService.ThrowIfInvalidModel(createTenantDTO);

            var createTenant = new CreateTenantWithAdmin
            {
                AdminId = executionContextAccessor.UserId,
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
            var tenantId = executionContextAccessor.TenantId;
            await commandDispatcher.DispatchAsync<DeleteTenant>(new DeleteTenant { });

            var userId = executionContextAccessor.UserId;
            var user = await queryDispatcher.DispatchAsync<GetUserById, ApplicationUser>(new GetUserById { });

            await signInManager.RefreshSignInAsync(user);
        }

        [HttpPost("memberships")]
        public async Task<ActionResult> CreateTenantMembership(InviteUserToTenantDTO inviteUserToGroupDTO)
        {
            var userId = executionContextAccessor.UserId;

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

