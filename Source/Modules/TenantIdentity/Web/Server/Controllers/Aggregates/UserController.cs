using Microsoft.AspNetCore.Mvc;
using Shared.Features.Server;
using System;

namespace Modules.TenantIdentity.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IServiceProvider serviceProvider) : base(serviceProvider) 
        {
        }
    }
}
