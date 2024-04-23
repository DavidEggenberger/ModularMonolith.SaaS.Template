using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.Features.Infrastructure.Configuration;
using Modules.Subscriptions.Features.Infrastructure.EFCore;
using Shared.Features.Messaging.Command;
using Shared.Kernel.BuildingBlocks.Auth;
using Stripe.Checkout;

namespace Modules.Subscriptions.Features.Infrastructure.StripePayments
{
    public class CreateStripeCheckoutSession : ICommand<Session>
    {
        public SubscriptionPlanType SubscriptionPlanType { get; set; }
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public string RedirectBaseUrl { get; set; }
    }

    public class CreateStripeCheckoutSessionCommandHandler : ICommandHandler<CreateStripeCheckoutSession, Stripe.Checkout.Session>
    {
        private readonly SubscriptionsDbContext subscriptionDbContext;
        private readonly SubscriptionsConfiguration subscriptionConfiguration;

        public CreateStripeCheckoutSessionCommandHandler(SubscriptionsDbContext subscriptionDbContext, SubscriptionsConfiguration subscriptionConfiguration)
        {
            this.subscriptionDbContext = subscriptionDbContext;
            this.subscriptionConfiguration = subscriptionConfiguration;
        }

        public async Task<Session> HandleAsync(CreateStripeCheckoutSession command, CancellationToken cancellationToken)
        {
            var subscriptionOwner = await subscriptionDbContext.StripeCustomers
                .FirstAsync(stripeCustomer => stripeCustomer.UserId == command.UserId);

            var subscription = subscriptionConfiguration.GetSubscriptionType(command.SubscriptionPlanType);

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                Customer = subscriptionOwner.StripePortalCustomerId,
                ClientReferenceId = subscriptionOwner.UserId.ToString(),
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = subscription.StripePriceId,
                        Quantity = 1
                    }
                },
                Mode = "subscription",
                SuccessUrl = command.RedirectBaseUrl + "api/striperedirect?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = command.RedirectBaseUrl + "api/striperedirect",
                SubscriptionData = new SessionSubscriptionDataOptions
                {
                    Metadata = new Dictionary<string, string>
                    {
                        ["UserId"] = command.UserId.ToString(),
                        ["TenantId"] = command.TenantId.ToString(),
                    },
                    TrialPeriodDays = subscription.TrialPeriodDays
                }
            };
            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return session;
        }
    }
}
