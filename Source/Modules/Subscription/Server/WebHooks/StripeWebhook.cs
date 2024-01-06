using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Modules.Subscription.DomainFeatures.Infrastructure;
using Shared.DomainFeatures;
using Modules.Subscription.DomainFeatures.Aggregates.StripeSubscriptionAggregate.Application.Commands.Subscription;
using Modules.Subscription.DomainFeatures.Infrastructure.Configuration;
using Modules.Subscriptions.Features.Aggregates.StripeSubscriptionAggregate.Application.Commands;

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
                        Subscription = subscription
                    };

                    await commandDispatcher.DispatchAsync(createTrialingSubscription);
                }
                // Sent each billing interval when a payment succeeds. (Stripe)
                else if (stripeEvent.Type == Events.InvoicePaid)
                {
                    var invoice = stripeEvent.Data.Object as Invoice;

                    var subscription = await new SubscriptionService().GetAsync(invoice.SubscriptionId);

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
