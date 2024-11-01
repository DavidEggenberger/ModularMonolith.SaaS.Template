using Microsoft.EntityFrameworkCore;
using Modules.Subscriptions.Features.Infrastructure.EFCore;
using Shared.Features.Messaging.Command;
using Stripe.BillingPortal;

namespace Modules.Subscriptions.Features.Infrastructure.StripePayments
{
    public class CreateStripeBillingPortalSession : Command<Stripe.BillingPortal.Session>
    {
        public Guid UserId { get; set; }
        public string RedirectBaseUrl { get; set; }
    }

    public class CreateStripeBillingPortalSessionCommandHandler : ICommandHandler<CreateStripeBillingPortalSession, Stripe.BillingPortal.Session>
    {
        private readonly SubscriptionsDbContext subscriptionDbContext;

        public CreateStripeBillingPortalSessionCommandHandler(SubscriptionsDbContext subscriptionDbContext)
        {
            this.subscriptionDbContext = subscriptionDbContext;
        }

        public async Task<Session> HandleAsync(CreateStripeBillingPortalSession command, CancellationToken cancellationToken)
        {
            var stripeCustomer = await subscriptionDbContext.StripeCustomers
               .FirstAsync(stripeCustomer => stripeCustomer.UserId == command.UserId);

            var options = new SessionCreateOptions
            {
                Customer = stripeCustomer.StripePortalCustomerId,
                ReturnUrl = command.RedirectBaseUrl
            };
            
            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return session;
        }
    }
}
