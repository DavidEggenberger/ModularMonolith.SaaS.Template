using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptions.Application.Queries;
using Shared.Features.Misc;
using Shared.Kernel.Constants.Auth;

namespace Modules.Subscriptions.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = PolicyConstants.TenantAdminPolicy)]
    public class StripeSubscriptionsController : BaseController
    {
        public StripeSubscriptionsController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        public async Task GetSubscription()
        {
            var getSubscriptionForTenant = new GetSubscriptionForTenant
            {

            };
        }
    }
}
