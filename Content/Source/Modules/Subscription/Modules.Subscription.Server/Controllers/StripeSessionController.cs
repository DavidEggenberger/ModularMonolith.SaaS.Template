using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers.Stripe
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeTeamAdmin]
    public class StripeSessionController : ControllerBase
    {
        private readonly string returnUrl;
        public StripeSessionController(IStripeSessionService stripeSessionService, IServerInformationProvider serverInformationProvider, IStripeSubscriptionService stripeSubscriptionService)
        {
            this.stripeSessionService = stripeSessionService;
            this.stripeSubscriptionService = stripeSubscriptionService;
            returnUrl = serverInformationProvider.BaseURI.AbsoluteUri;
        }

        public async Task<IActionResult> CancelSubscription()
        {
            return Ok();
        }

        [HttpPost("Subscribe/Premium")]
        public async Task<ActionResult> RedirectToStripePremiumSubscription()
        {
            var premiumSubscription = stripeSubscriptionService.GetSubscriptionType(SubscriptionPlanType.Professional);
            var checkoutSession = await stripeSessionService.CreateCheckoutSessionAsync(returnUrl, ApplicationUser, Tenant.Id, premiumSubscription);

            Response.Headers.Add("Location", checkoutSession.Url);
            return new StatusCodeResult(303);
        }

        [HttpPost("Subscribe/Enterprise")]
        public async Task<ActionResult> RedirectToStripeEnterpriseSubscription()
        {
            var enterpriseSubscription = stripeSubscriptionService.GetSubscriptionType(SubscriptionPlanType.Enterprise);
            var checkoutSession = await stripeSessionService.CreateCheckoutSessionAsync(returnUrl, ApplicationUser, Tenant.Id, enterpriseSubscription);

            Response.Headers.Add("Location", checkoutSession.Url);
            return new StatusCodeResult(303);
        }

        [Route("create-portal-session")]
        [HttpPost]
        public async Task<ActionResult> Create()
        {
            var billingPortalSession = await stripeSessionService.CreateBillingPortalSessionAsync(returnUrl, ApplicationUser.StripeCustomerId);

            Response.Headers.Add("Location", billingPortalSession.Url);
            return new StatusCodeResult(303);
        }
    }
}
