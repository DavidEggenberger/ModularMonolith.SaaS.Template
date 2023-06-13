//using Shared.Modules.Layers.Application.CQRS.Command;
//using Shared.Modules.Layers.Infrastructure.StripeIntegration;
//using Shared.Modules.Layers.Infrastructure.StripeIntegration.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Shared.Infrastructure.CQRS.Command;
using Modules.Subscription.DomainFeatures.Infrastructure;

namespace Modules.Subscription.Server.WebHooks
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeWebhook : ControllerBase
    {
        private readonly StripeOptions stripeOptions;
        private readonly ICommandDispatcher commandDispatcher;
        public StripeWebhook(IOptions<StripeOptions> stripeOptions, ICommandDispatcher commandDispatcher)
        {
            this.stripeOptions = stripeOptions.Value;
            this.commandDispatcher = commandDispatcher;
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

                //More Events Events.Checkout...
                if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    await commandDispatcher.DispatchAsync(new CreateSubscriptionCommand { Subscription = subscription });
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionUpdated)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    await commandDispatcher.DispatchAsync(new UpdateSubscriptionCommand { Subscription = subscription });
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionDeleted)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    await commandDispatcher.DispatchAsync(new DeleteSubscriptionCommand { Subscription = subscription });
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionTrialWillEnd)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    await commandDispatcher.DispatchAsync(new SubscriptionTrialEndedCommand { Subscription = subscription });
                }
                return Ok();
            }
            catch (StripeException e)
            {
                throw new StripeIntegrationException(e.Message);
            }
        }
    }
}
