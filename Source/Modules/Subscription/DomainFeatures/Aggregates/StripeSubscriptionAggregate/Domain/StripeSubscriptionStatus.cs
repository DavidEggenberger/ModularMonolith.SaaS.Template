namespace Modules.Subscription.Features.Aggregates.StripeSubscriptionAggregate.Domain
{
    public enum StripeSubscriptionStatus
    {
        Active,
        Canceled,
        Trialing,
        Unpaid
    }
}
