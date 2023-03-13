using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Authentication;
using System.Linq;

namespace Server.Modules.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BFFUserInfoController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<BFFUserInfoDTO> GetCurrentUser()
        {
            if (User.Identity.IsAuthenticated is false)
            {
                return BFFUserInfoDTO.Anonymous;
            }

            return new BFFUserInfoDTO()
            {
                Claims = User.Claims.Select(claim => new ClaimValueDTO { Type = claim.Type, Value = claim.Value }).ToList()
            };
        }
    }
}
