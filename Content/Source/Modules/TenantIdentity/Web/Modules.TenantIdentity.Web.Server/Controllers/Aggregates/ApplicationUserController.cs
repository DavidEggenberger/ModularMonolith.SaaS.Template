using AutoMapper;
using Shared.Infrastructure.CQRS.Query;
using Shared.Modules.Layers.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Web.Server;

namespace Modules.TenantIdentity.Web.Server.Controllers.Aggregates
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationUserManager applicationUserManager;
        public ApplicationUserController(SignInManager<ApplicationUser> signInManager, ApplicationUserManager applicationUserManager, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.applicationUserManager = applicationUserManager;
            this.mapper = mapper;
        }



        //[HttpGet("selectedTeam")]
        //public async Task<TenantDTO> GetSelectedTeamForUser()
        //{
        //    ApplicationUser applicationUser = await applicationUserManager.FindByClaimsPrincipalAsync(HttpContext.User);

        //    //var tenantByIdQuery = new GetTenantByQuery() { TenantId = applicationUser.SelectedTenantId };
        //    //Tenant currentTenant = await queryDispatcher.DispatchAsync<GetTenantByQuery, Tenant>(tenantByIdQuery);

        //    return mapper.Map<TenantDTO>(currentTenant);
        //}
    }
}
