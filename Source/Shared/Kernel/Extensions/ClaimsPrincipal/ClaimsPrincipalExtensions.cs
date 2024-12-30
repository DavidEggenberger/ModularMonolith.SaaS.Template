using Shared.Kernel.BuildingBlocks.Auth.Constants;
using Shared.Kernel.DomainKernel;
using Shared.Kernel.Extensions.ClaimsPrincipal.Exceptions;
using System.ComponentModel;

namespace Shared.Kernel.Extensions.ClaimsPrincipal
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasUserIdClaim(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.HasClaim(c => c.Type == ClaimConstants.UserIdClaimType);
        }

        public static T GetUserId<T>(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFrom(claimsPrincipal?.FindFirst(ClaimConstants.UserIdClaimType).Value);
        }

        public static bool HasTenantIdClaim(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.HasClaim(c => c.Type == ClaimConstants.TenantIdClaimType);
        }

        public static T GetTenantId<T>(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFrom(claimsPrincipal?.FindFirst(ClaimConstants.TenantPlanClaimType).Value);
        }

        public static TenantRole GetTenantRole(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            return (TenantRole)Enum.Parse(typeof(TenantRole), claimsPrincipal?.FindFirst(ClaimConstants.UserRoleInTenantClaimType).Value);
        }

        public static SubscriptionPlanType GetTenantSubscriptionPlanType(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            return (SubscriptionPlanType)Enum.Parse(typeof(SubscriptionPlanType), claimsPrincipal?.FindFirst(ClaimConstants.TenantPlanClaimType).Value);
        }

        public static string GetRoleClaim(this System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.GetClaimValue(ClaimConstants.UserRoleInTenantClaimType);
        }

        public static string GetClaimValue(this System.Security.Claims.ClaimsPrincipal claimsPrincipal, string claimType)
        {
            try
            {
                return claimsPrincipal.FindFirst(claimType)?.Value;
            }
            catch (Exception _)
            {
                throw new ClaimNotFoundException();
            }
        }
    }
}
