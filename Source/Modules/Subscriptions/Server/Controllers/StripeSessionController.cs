using Microsoft.AspNetCore.Mvc;
using Shared.Kernel.BuildingBlocks.Authorization;
using Shared.Kernel.BuildingBlocks.Authorization.Attributes;
using Shared.Features;
using Modules.Subscriptions.Features.Agregates.StripeSubscriptionAggregate.Application.Commands;

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
                TenantId = executionContextAccessor.TenantId,
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
                RedirectBaseUrl = webContextAccessor.BaseURI.AbsoluteUri
            };
            var billingPortalSession = await commandDispatcher.DispatchAsync<CreateStripeBillingPortalSession, Stripe.BillingPortal.Session>(createBillingPortalSession);

            Response.Headers.Add("Location", billingPortalSession.Url);
            return new StatusCodeResult(303);
        }
    }
}
