using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;
using Shared.Kernel.BuildingBlocks.Authorization.Attributes;
using System.Threading.Tasks;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using System.Collections.Generic;
using Shared.Web.Server;
using System;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant;
using Modules.TenantIdentity.Web.Shared.DTOs.Aggregates.Tenant.Operations;
using Modules.TenantIdentity.DomainFeatures.Application.Queries;
using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Application.Commands;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Application.Queries;
using Modules.TenantIdentity.DomainFeatures.TenantAggregate.Application.Queries;
using System.Linq;
using Modules.TenantIdentity.IntegrationEvents;

namespace Modules.TenantIdentity.Web.Server.Controllers.Aggregates
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : BaseController
    {
        private readonly SignInManager<User> signInManager;

        public TenantsController(SignInManager<User> signInManager, IServiceProvider serviceProvider) : base(serviceProvider)
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
        [Authorize]
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

            var user = await queryDispatcher.DispatchAsync<GetUserById, User>(new GetUserById { });
            await signInManager.RefreshSignInAsync(user);
            
            return CreatedAtAction(nameof(CreateTenant), createdTenant);
        }

        [HttpDelete("{id}")]
        public async Task DeleteTenant(Guid id)
        {
            var tenantId = executionContextAccessor.TenantId;
            await commandDispatcher.DispatchAsync<DeleteTenant>(new DeleteTenant { });

            var userId = executionContextAccessor.UserId;
            var user = await queryDispatcher.DispatchAsync<GetUserById, User>(new GetUserById { });

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

