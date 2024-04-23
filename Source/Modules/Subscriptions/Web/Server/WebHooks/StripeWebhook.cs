using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Shared.Features.Server;
using Modules.Subscriptions.Features.Infrastructure.Configuration;
using Modules.Subscriptions.Features.DomainFeatures.StripeSubscriptionAggregate.Application.Commands;

namespace Modules.Subscriptions.Server.WebHooks
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeWebhook : BaseController
    {
        private readonly SubscriptionsConfiguration subscriptionConfiguration;
        public StripeWebhook(SubscriptionsConfiguration subscriptionConfiguration, IServiceProvider serviceProvider) : base(serviceProvider)
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
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;

                    var subscription = await new SubscriptionService().GetAsync(session.SubscriptionId);
                    var userId = subscription.Metadata["UserId"];

                    var createTrialingSubscription = new CreateTrialingSubscription
                    {
                        UserId = Guid.Parse(userId),
                        StripeCustomerId = subscription.CustomerId,
                        CreatedStripeSubscription = subscription
                    };

                    await commandDispatcher.DispatchAsync(createTrialingSubscription);
                }
                // Sent each billing interval when a payment succeeds. (Stripe)
                else if (stripeEvent.Type == Events.InvoicePaid)
                {
                    var invoice = stripeEvent.Data.Object as Invoice;

                    var subscription = await new SubscriptionService().GetAsync(invoice.SubscriptionId);

                    var updateSubscriptionPeriod = new UpdateSubscriptionPeriod
                    {
                        
                    };


                }
                // Sent each billing interval if there is an issue with your customer’s payment method. (Stripe)
                else if (stripeEvent.Type == Events.InvoicePaymentFailed)
                {
                    var invoice = stripeEvent.Data.Object as Invoice;

                    var subscription = await new SubscriptionService().GetAsync(invoice.SubscriptionId);


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
