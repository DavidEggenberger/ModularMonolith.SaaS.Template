using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Shared.Features;
using Modules.Subscriptions.Features.Infrastructure.StripePayments;
using Modules.Subscriptions.Features.Application.Commands.StripeSubscriptionAggregate;
using Modules.Subscription.Features.Infrastructure.Configuration;

namespace Modules.Subscription.Server.WebHooks
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeWebhook : BaseController
    {
        private readonly SubscriptionConfiguration subscriptionConfiguration;
        public StripeWebhook(SubscriptionConfiguration subscriptionConfiguration, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.subscriptionConfiguration = subscriptionConfiguration;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json, false);
                var signatureHeader = Request.Headers["Stripe-Signature"];
                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, subscriptionConfiguration.StripeEndpointSecret, throwOnApiVersionMismatch: false);

                // Minimum Events copied from https://stripe.com/docs/billing/subscriptions/build-subscriptions
                // Sent when a customer clicks the Pay or Subscribe button in Checkout, informing you of a new purchase. (Stripe)
                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    await commandDispatcher.DispatchAsync(new CreateSubscriptionForTenant { Subscription = subscription });
                }
                // Sent each billing interval when a payment succeeds. (Stripe)
                else if (stripeEvent.Type == Events.InvoicePaid)
                {
                    var subscription = stripeEvent.Data.Object as Stripe.Subscription;
                    await commandDispatcher.DispatchAsync(new UpdateSubscription { Subscription = subscription });
                }
                // Sent each billing interval if there is an issue with your customer’s payment method. (Stripe)
                else if (stripeEvent.Type == Events.InvoicePaymentFailed)
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
