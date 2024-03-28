using Microsoft.AspNetCore.Mvc;
using Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptionAggregate.Application.Queries;
using Shared.Features.Server;
using Shared.Kernel.BuildingBlocks.Auth.Attributes;

namespace Modules.Subscriptions.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeSubscriptionsController : BaseController
    {
        public StripeSubscriptionsController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        [AuthorizeTenantAdmin]
        public async Task GetSubscription()
        {
            var getSubscriptionForTenant = new GetSubscriptionForTenant
            {

            };
        }
    }
}
