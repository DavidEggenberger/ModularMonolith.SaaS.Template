using Shared.Modules.Layers.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Modules.Layers.Application.CQRS.Command;
using Module.Shared.Modules.Layers.Infrastructure;
using Shared.Modules.Layers.Infrastructure.Identity.Commands;
using Shared.SharedKernel.Constants;
using ApplicationUserManager = Shared.Modules.Layers.Infrastructure.Identity.ApplicationUserManager;
using Shared.SharedKernel.Exstensions;

namespace Modules.TenantIdentity.Web.Server.Controllers.IdentityOperations
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class ExternalLoginCallbackController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationUserManager userManager;
        private readonly ICommandDispatcher commandDispatcher;
        public ExternalLoginCallbackController(SignInManager<ApplicationUser> signInManager, ApplicationUserManager userManager, ICommandDispatcher commandDispatcher)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.commandDispatcher = commandDispatcher;
        }

        [HttpGet("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();
            var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (info is not null && user is null)
            {
                ApplicationUser _user = new ApplicationUser
                {
                    UserName = info.Principal.Identity.Name,
                    Email = info.Principal.GetClaimValue(ClaimConstants.EmailClaimType),
                    PictureUri = info.Principal.GetClaimValue(ClaimConstants.PictureClaimType)
                };

                var createUserCommand = new CreateUserCommand { User = _user };
                await commandDispatcher.DispatchAsync(createUserCommand);
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: false);
            return signInResult switch
            {
                Microsoft.AspNetCore.Identity.SignInResult { Succeeded: true } => LocalRedirect("/"),
                Microsoft.AspNetCore.Identity.SignInResult { RequiresTwoFactor: true } => RedirectToPage("/TwoFactorLogin", new { ReturnUrl = returnUrl }),
                _ => LocalRedirect("/")
            };
        }
    }
}
