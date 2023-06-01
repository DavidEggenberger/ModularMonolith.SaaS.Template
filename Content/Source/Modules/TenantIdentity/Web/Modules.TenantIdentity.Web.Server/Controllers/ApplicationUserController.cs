using AutoMapper;
using Shared.Infrastructure.CQRS.Query;
using Shared.Modules.Layers.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationUserManager applicationUserManager;
        private readonly IMapper mapper;
        private readonly IQueryDispatcher queryDispatcher;
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
