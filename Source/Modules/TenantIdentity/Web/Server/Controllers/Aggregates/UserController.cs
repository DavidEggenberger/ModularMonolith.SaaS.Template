using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure;
using System;

namespace Modules.TenantIdentity.Web.Server.Controllers.Aggregates
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
