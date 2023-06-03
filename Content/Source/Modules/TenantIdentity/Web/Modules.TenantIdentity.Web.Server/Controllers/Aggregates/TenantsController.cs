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

namespace Modules.TenantIdentity.Web.Server.Controllers.Aggregates
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeTenantAdmin]
    public class TenantsController : BaseController
    {
        private readonly SignInManager<User> signInManager;

        public TenantsController(SignInManager<User> signInManager, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<TenantDTO>> CreateTeam(TenantDTO team)
        {
            User applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);

            await signInManager.RefreshSignInAsync(applicationUser);
            return CreatedAtAction("CreateTeam", team);
        }

        [HttpGet]
        public async Task<TenantDetailDTO> GetAdminInfo()
        {
            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
            TeamMetrics teamMetrics = teamManager.GetMetricsForTeam(team);
            TeamAdminInfoDTO teamAdminInfoDTO = mapper.Map<TeamAdminInfoDTO>(team);
            teamAdminInfoDTO.Metrics = mapper.Map<TeamMetricsDTO>(teamMetrics);
            return teamAdminInfoDTO;
        }


        [HttpGet("allTeams")]
        public async Task<IEnumerable<TenantDTO>> GetAllTeamsForUser()
        {
            User applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);
            List<Team> teamMemberships = applicationUserManager.GetAllTeamsWhereUserIsMember(applicationUser);
            return teamMemberships.Select(x => mapper.Map<TeamDTO>(x));
        }

        [HttpPost("invite")]
        public async Task<ActionResult> InviteUsers(InviteMembersDTO inviteUserToGroupDTO)
        {
            Tenant tenant = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
            await teamManager.InviteMembersAsync(team, inviteUserToGroupDTO.Emails);
            return Ok();
        }

        [HttpPost("invite/revoke")]
        public async Task<ActionResult> RevokeInvitation(RevokeInvitationDTO revokeInvitationDTO)
        {
            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
            User applicationUser = await applicationUserManager.FindByIdAsync(revokeInvitationDTO.UserId);
            await teamManager.RemoveInvitationAsync(team, applicationUser);
            return Ok();
        }

        [HttpPost("changerole")]
        public async Task<ActionResult> ChangeRoleOfTeamMember(ChangeRoleOfMemberDTO changeRoleOfTeamMemberDTO)
        {
            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
            User applicationUser = await applicationUserManager.FindByIdAsync(changeRoleOfTeamMemberDTO.UserId);
            await teamManager.ChangeRoleOfMemberAsync(applicationUser, team, (TeamRole)changeRoleOfTeamMemberDTO.TargetRole);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task DeleteTeam(Guid id)
        {
            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
            User applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);
            if (team.CreatorId == applicationUser.Id)
            {
                await teamManager.DeleteAsync(team);
            }
            await signInManager.RefreshSignInAsync(applicationUser);
        }

        [HttpDelete("removeMember/{id}")]
        public async Task RemoveMember(Guid id)
        {
            User applicationUser = await applicationUserManager.FindByIdAsync(id);
            Team team = await teamManager.FindByClaimsPrincipalAsync(HttpContext.User);
            await teamManager.RemoveMemberAsync(team, applicationUser);
        }
    }
}

