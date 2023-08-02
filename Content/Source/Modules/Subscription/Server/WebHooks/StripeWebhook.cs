using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Shared.Infrastructure.CQRS.Command;
using Modules.Subscription.DomainFeatures.Infrastructure;
using Shared.Web.Server;
using Modules.Subscription.DomainFeatures.StripeSubscriptionAggregate.Application.Commands.Subscription;

namespace Modules.Subscription.Server.WebHooks
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeWebhook : BaseController
    {
        private readonly StripeOptions stripeOptions;
        public StripeWebhook(IOptions<StripeOptions> stripeOptions, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.stripeOptions = stripeOptions.Value;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];
                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, stripeOptions.EndpointSecret);

                if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    await commandDispatcher.DispatchAsync(new CreateSubscriptionForTenant { Subscription = subscription });
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionUpdated)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    await commandDispatcher.DispatchAsync(new UpdateSubscription { Subscription = subscription });
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionDeleted)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    await commandDispatcher.DispatchAsync(new DeleteSubscription { Subscription = subscription });
                }
                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(e);
            }
        }
    }
}
