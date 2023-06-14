using Microsoft.AspNetCore.Mvc;
using Shared.Kernel.BuildingBlocks;
using Shared.Kernel.BuildingBlocks.Authorization.Attributes;

namespace WebServer.Controllers.Stripe
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeTenantAdmin]
    public class StripeSessionController : ControllerBase
    {
        private readonly string returnUrl;
        public StripeSessionController(IWebContextAccessor webContextInformationProvider)
        {
            returnUrl = webContextInformationProvider.BaseURI.AbsoluteUri;
        }

        [HttpPost("checkout/Premium")]
        public async Task<ActionResult> RedirectToStripePremiumSubscription()
        {
            var premiumSubscription = stripeSubscriptionService.GetSubscriptionType(SubscriptionPlanType.Professional);
            var checkoutSession = await stripeSessionService.CreateCheckoutSessionAsync(returnUrl, ApplicationUser, Tenant.Id, premiumSubscription);

            Response.Headers.Add("Location", checkoutSession.Url);
            return new StatusCodeResult(303);
        }

        [HttpPost("checkout/Enterprise")]
        public async Task<ActionResult> RedirectToStripeEnterpriseSubscription()
        {
            var enterpriseSubscription = stripeSubscriptionService.GetSubscriptionType(SubscriptionPlanType.Enterprise);
            var checkoutSession = await stripeSessionService.CreateCheckoutSessionAsync(returnUrl, ApplicationUser, Tenant.Id, enterpriseSubscription);

            Response.Headers.Add("Location", checkoutSession.Url);
            return new StatusCodeResult(303);
        }

        [Route("/portal-session")]
        [HttpPost]
        public async Task<ActionResult> Create()
        {
            var billingPortalSession = await stripeSessionService.CreateBillingPortalSessionAsync(returnUrl, ApplicationUser.StripeCustomerId);

            Response.Headers.Add("Location", billingPortalSession.Url);
            return new StatusCodeResult(303);
        }
    }
}
