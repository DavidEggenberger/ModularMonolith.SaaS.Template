﻿using Shared.Kernel.BuildingBlocks.Authorization;

namespace Modules.Subscription.DomainFeatures.Infrastructure
{
    public class StripeSubscriptionPlan
    {
        public string StripePriceId { get; set; }
        public SubscriptionPlanType Type { get; set; }
    }
}