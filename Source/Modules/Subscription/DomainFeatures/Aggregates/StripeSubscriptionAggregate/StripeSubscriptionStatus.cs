namespace Modules.Subscriptions.Features.Aggregates.StripeSubscriptionAggregate
{
    public enum StripeSubscriptionStatus
    {
        Active,
        Canceled,
        Trialing,
        TrialEnded,
        Unpaid
    }
}
