namespace Shared.Kernel.Constants
{
    public class EndpointConstants
    {
        public static class TenantIdentity
        {
            public const string UsersEndpoint = "/api/users";
            public const string IdentityOperationsEndpoint = "/api/identityoperations";

            public const string IdentityAccountPath = "/Identity/Account";
            public const string SignUpPath = "Identity/SignUp";
            public const string LoginPath = "Identity/Login";
            public const string LogoutPath = "api/user/Logout";
            public const string UserClaimsPath = "api/user";
        }

        public static class Subscriptions
        {
            public const string StripePremiumSubscriptionPath = "/api/stripe/subscribe/premium";
            public const string StripeEnterpriseSubscriptionPath = "/api/stripe/subscribe/enterprise";
        }
    }
}
