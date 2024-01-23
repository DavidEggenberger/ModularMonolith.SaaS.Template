using Microsoft.AspNetCore.Mvc;
using Shared.Kernel.BuildingBlocks.Auth;
using Shared.Kernel.BuildingBlocks.Auth.Attributes;
using Shared.Features;
using Modules.Subscriptions.Features.Infrastructure.StripePayments;

namespace Modules.Subscription.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeTenantAdmin]
    public class StripeSessionController : BaseController
    {
        public StripeSessionController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            
        }

        [HttpPost("checkout/{subscriptionPlanType}")]
        public async Task<ActionResult> RedirectToStripePremiumSubscription([FromRoute] SubscriptionPlanType subscriptionPlanType)
        {
            var createStripeCheckoutSession = new CreateStripeCheckoutSession 
            { 
                SubscriptionPlanType = subscriptionPlanType,
                UserId = executionContextAccessor.ExecutionContext.UserId,
                TenantId = executionContextAccessor.ExecutionContext.TenantId,
                RedirectBaseUrl = webContextAccessor.BaseURI.AbsoluteUri
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
                UserId = executionContextAccessor.ExecutionContext.UserId,
                RedirectBaseUrl = webContextAccessor.BaseURI.AbsoluteUri,
            };
            var billingPortalSession = await commandDispatcher.DispatchAsync<CreateStripeBillingPortalSession, Stripe.BillingPortal.Session>(createBillingPortalSession);

            Response.Headers.Add("Location", billingPortalSession.Url);
            return new StatusCodeResult(303);
        }
    }
}
