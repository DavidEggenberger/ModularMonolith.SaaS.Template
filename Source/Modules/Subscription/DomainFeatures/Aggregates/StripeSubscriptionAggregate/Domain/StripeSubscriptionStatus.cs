namespace Modules.Subscription.DomainFeatures.Aggregates.StripeSubscriptionAggregate.Domain
{
    public enum StripeSubscriptionStatus
    {
        Active,
        Canceled,
        Trialing,
        Unpaid
    }
}
