using Microsoft.EntityFrameworkCore;
using Modules.Subscription.Features.Infrastructure.EFCore;
using Shared.Features.CQRS.Command;
using Stripe.BillingPortal;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Modules.Subscriptions.Features.Infrastructure.StripePayments
{
    public class CreateStripeBillingPortalSession : ICommand<Stripe.BillingPortal.Session>
    {
        public Guid UserId { get; set; }
        public string RedirectBaseUrl { get; set; }
    }

    public class CreateStripeBillingPortalSessionCommandHandler : ICommandHandler<CreateStripeBillingPortalSession, Stripe.BillingPortal.Session>
    {
        private readonly SubscriptionDbContext subscriptionDbContext;

        public CreateStripeBillingPortalSessionCommandHandler(SubscriptionDbContext subscriptionDbContext)
        {
            this.subscriptionDbContext = subscriptionDbContext;
        }

        public async Task<Session> HandleAsync(CreateStripeBillingPortalSession command, CancellationToken cancellationToken)
        {
            var stripeCustomer = await subscriptionDbContext.StripeCustomers
               .FirstAsync(stripeCustomer => stripeCustomer.UserId == command.UserId);

            var options = new SessionCreateOptions
            {
                Customer = stripeCustomer.StripeCustomerId,
                ReturnUrl = command.RedirectBaseUrl
            };
            
            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return session;
        }
    }
}
