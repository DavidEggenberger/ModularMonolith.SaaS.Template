//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;


//namespace WebServer.Controllers.Stripe
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StripeSuccessController : ControllerBase
//    {
//        //private readonly IStripeSessionService stripeSessionService;
//        //private readonly IQueryDispatcher queryDispatcher;
//        //private readonly SignInManager<ApplicationUser> signInManager;
//        //public StripeSuccessController()
//        //{

//        //}

//        //[HttpGet("/order/success")]
//        //public async Task<ActionResult> OrderSuccess([FromQuery] string session_id)
//        //{
//        //    var stripeCheckoutSession = await stripeSessionService.GetStripeCheckoutSessionAsync(session_id);

//        //    var user = await queryDispatcher.DispatchAsync<UserByStripeCustomerIdQuery, ApplicationUser>(new UserByStripeCustomerIdQuery { StripeCustomerId = stripeCheckoutSession.CustomerId });

//        //    await signInManager.SignInAsync(user, true);

//        //    return LocalRedirect("/");
//        //}
//    }
//}
