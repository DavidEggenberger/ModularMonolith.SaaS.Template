using Shared.Infrastructure.CQRS.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Web.Server;
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
