using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Kernel.BuildingBlocks.Auth.Constants;
using Shared.Kernel.Extensions.ClaimsPrincipal;
using System;
using System.Threading.Tasks;
using Modules.TenantIdentity.Features.DomainFeatures.Users;
using Modules.TenantIdentity.Features.DomainFeatures.Users.Application.Commands;
using Shared.Features.Misc;

namespace Modules.TenantIdentity.Web.Server.Controllers.Infrastructure
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class ExternalLoginCallbackController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        public ExternalLoginCallbackController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();

            var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            if (info is not null && user is null)
            {
                var name = info.Principal.Identity.Name;
                var mail = info.Principal.GetClaimValue(ClaimConstants.EmailClaimType);
                var pictureUri = info.Principal.GetClaimValue(ClaimConstants.PictureClaimType);

                ApplicationUser _user = ApplicationUser.Create(name, mail, pictureUri);

                var createUserCommand = new CreateNewUser { User = _user };
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
