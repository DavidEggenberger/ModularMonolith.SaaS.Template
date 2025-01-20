using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Modules.TenantIdentity.Features.DomainFeatures.Users;
using Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Queries;
using Modules.TenantIdentity.Features.DomainFeatures.Tenants.Application.Commands;
using Modules.TenantIdentity.Features.DomainFeatures.Users.Application.Queries;
using Modules.TenantIdentity.Public.DTOs.Tenant.Operations;
using Modules.TenantIdentity.Public.DTOs.Tenant;
using Shared.Features.Misc;
using Shared.Kernel.Constants.Auth;

namespace Modules.TenantIdentity.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = PolicyConstants.TenantAdminPolicy)]
    [ApiController]
    public class TenantsController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public TenantsController(SignInManager<ApplicationUser> signInManager, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.signInManager = signInManager;
        }

        [HttpGet("{tenantId}")]
        public async Task<ActionResult<TenantDTO>> GetTenant([FromRoute] Guid tenantId)
        {
            var tenant = await queryDispatcher.DispatchAsync<GetTenant, TenantDTO>(new GetTenant { TenantId = tenantId });

            return Ok(tenant);
        }

        [HttpPost]
        public async Task<ActionResult<TenantDTO>> CreateTenant(CreateTenantDTO createTenantDTO)
        {
            validationService.ThrowIfInvalidModel(createTenantDTO);

            var createTenant = new CreateTenant
            {
                AdminId = executionContext.UserId,
                Name = createTenantDTO.Name
            };
            var createdTenant = await commandDispatcher.DispatchAsync<CreateTenant, TenantDTO>(createTenant);

            var user = await queryDispatcher.DispatchAsync<GetExecutingUser, ApplicationUser>(new GetExecutingUser());
            await signInManager.RefreshSignInAsync(user);
            
            return CreatedAtAction(nameof(CreateTenant), createdTenant);
        }

        [HttpDelete("{tenantId}")]
        public async Task<ActionResult> DeleteTenant([FromRoute] Guid tenantId)
        {
            var deleteTenant = new DeleteTenant
            {
                TenantId = tenantId
            };

            await commandDispatcher.DispatchAsync<DeleteTenant>(deleteTenant);

            return Ok();
        }

        [HttpPost("{tenantId}/memberships")]
        public async Task<ActionResult> AddMemberToTenant([FromRoute] Guid tenantId, InviteUserToTenantDTO inviteUserToGroupDTO)
        {
            var addMember = new AddMemberToTenant
            {
                UserId = inviteUserToGroupDTO.UserId,
                TenantId = inviteUserToGroupDTO.TenantId,
                Role = inviteUserToGroupDTO.Role,
            };

            await commandDispatcher.DispatchAsync<AddMemberToTenant>(null);
            
            return Ok();
        }

        [HttpPut("{tenantId}/memberships/{userId}")]
        public async Task<ActionResult> UpdateTenantMembership([FromRoute] Guid tenantId, [FromRoute] Guid userId, ChangeRoleOfTenantMemberDTO changeRoleOfTeamMemberDTO)
        {
            var updateTenantMembership = new UpdateTenantMembership
            {
                TenantId = tenantId,
                Role = changeRoleOfTeamMemberDTO.Role,
                UserId = userId
            };

            await commandDispatcher.DispatchAsync<UpdateTenantMembership>(updateTenantMembership);

            return Ok();
        }

        [HttpDelete("{tenantId}/memberships/{userId}")]
        public async Task<ActionResult> RemoveMemberFromTenant([FromRoute] Guid tenantId, [FromRoute] Guid userId)
        {
            var removeMember = new RemoveMemberFromTenant
            {
                TenantId = tenantId,
                UserId = userId
            };

            await commandDispatcher.DispatchAsync<RemoveMemberFromTenant>(removeMember);

            return Ok();
        }
    }
}

