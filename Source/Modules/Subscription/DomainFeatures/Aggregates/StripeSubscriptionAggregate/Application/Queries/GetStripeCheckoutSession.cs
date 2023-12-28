﻿using Shared.Features.CQRS.Query;

namespace Modules.Subscription.Features.Aggregates.StripeSubscriptionAggregate.Application.Queries
{
    public class GetStripeCheckoutSession : IQuery<Stripe.Checkout.Session>
    {
        public string SessionId { get; set; }
    }
}
