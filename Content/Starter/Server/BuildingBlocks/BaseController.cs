using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Server.Modules.Identity;
using System;

namespace Server.BuildingBlocks
{
    public class BaseController : ControllerBase
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        public BaseController(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }
    }
}
