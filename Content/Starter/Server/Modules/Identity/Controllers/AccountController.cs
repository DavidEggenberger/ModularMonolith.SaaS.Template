using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.BuildingBlocks;
using Server.Modules.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Server.Modules.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            
        }

        [Authorize]
        [HttpGet("Logout")]
        public async Task<ActionResult> LogoutCurrentUser(string redirectUri = "")
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return LocalRedirect(string.IsNullOrWhiteSpace(redirectUri) ? "/" : redirectUri);
        }
    }
}
