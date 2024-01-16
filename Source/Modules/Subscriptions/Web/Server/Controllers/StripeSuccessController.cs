using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Kernel.BuildingBlocks.Auth;
using Shared.Features;
using Modules.Subscriptions.Features.Domain.StripeSubscriptionAggregate.Queries;

namespace Modules.Subscription.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeSuccessController : BaseController
    {
        private readonly SignInManager<IUser> signInManager;
        private readonly UserManager<IUser> userManager;

        public StripeSuccessController(SignInManager<IUser> signInManager, UserManager<IUser> userManager, IServiceProvider serviceProvider) : base(serviceProvider) 
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet("/order/success")]
        public async Task<ActionResult> OrderSuccess([FromQuery] string session_id)
        {
            var getStripeCheckoutSession = new GetStripeCheckoutSession() { SessionId = session_id };
            var stripeCheckoutSession = await queryDispatcher.DispatchAsync<GetStripeCheckoutSession, Stripe.Checkout.Session>(getStripeCheckoutSession);

            //var getStripeCustomer = new GetStripeCustomer() { StripeCustomerId = stripeCheckoutSession.CustomerId };
            //var stripeCustomer = await queryDispatcher.DispatchAsync<GetStripeCustomer, StripeCustomer>(getStripeCustomer);

            //var user = await userManager.FindByIdAsync(stripeCustomer.UserId.ToString());

            //await signInManager.SignInAsync(user, true);

            return LocalRedirect("/");
        }
    }
}
