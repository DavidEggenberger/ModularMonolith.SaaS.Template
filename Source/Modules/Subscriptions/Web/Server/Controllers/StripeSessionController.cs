using Microsoft.AspNetCore.Mvc;
using Modules.Subscriptions.Features.Infrastructure.StripePayments;
using Shared.Features.Server;
using Shared.Kernel.DomainKernel;
using Microsoft.AspNetCore.Authorization;
using Shared.Kernel.BuildingBlocks.Auth.Constants;

namespace Modules.Subscriptions.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = PolicyConstants.TenantAdminPolicy)]
    public class StripeSessionController : BaseController
    {
        public StripeSessionController(IServiceProvider serviceProvider) : base(serviceProvider) { }

        [HttpPost("checkout/{subscriptionPlanType}")]
        public async Task<ActionResult> RedirectToStripePremiumSubscription([FromRoute] SubscriptionPlanType subscriptionPlanType)
        {
            var createStripeCheckoutSession = new CreateStripeCheckoutSession 
            { 
                SubscriptionPlanType = subscriptionPlanType,
                UserId = executionContext.UserId,
                TenantId = executionContext.TenantId,
                RedirectBaseUrl = executionContext.BaseURI.AbsoluteUri
            };
            var checkoutSession = await commandDispatcher.DispatchAsync<CreateStripeCheckoutSession, Stripe.Checkout.Session>(createStripeCheckoutSession);

            Response.Headers.Add("Location", checkoutSession.Url);
            return new StatusCodeResult(303);
        }

        [Route("/portal-session")]
        [HttpPost]
        public async Task<ActionResult> Create()
        {
            var createBillingPortalSession = new CreateStripeBillingPortalSession
            {
                UserId = executionContext.UserId,
                RedirectBaseUrl = executionContext.BaseURI.AbsoluteUri,
            };
            var billingPortalSession = await commandDispatcher.DispatchAsync<CreateStripeBillingPortalSession, Stripe.BillingPortal.Session>(createBillingPortalSession);

            Response.Headers.Add("Location", billingPortalSession.Url);
            return new StatusCodeResult(303);
        }
    }
}
