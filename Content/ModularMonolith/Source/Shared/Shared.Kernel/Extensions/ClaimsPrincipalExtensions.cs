using System.ComponentModel;
using System.Security.Claims;

namespace Shared.Kernel.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasUserIdClaim(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.HasClaim(c => c.Type == ClaimConstants.UserIdClaimType);
        }

        public static T GetUserId<T>(this ClaimsPrincipal claimsPrincipal)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFrom(claimsPrincipal?.FindFirst(ClaimConstants.UserIdClaimType).Value);
        }

        public static bool HasTenantIdClaim(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.HasClaim(c => c.Type == ClaimConstants.TenantIdClaimType);
        }

        public static T GetTenantId<T>(this ClaimsPrincipal claimsPrincipal)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFrom(claimsPrincipal?.FindFirst(ClaimConstants.TenantPlanClaimType).Value);
        }

        public static string GetRoleClaim(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.GetClaimValue(ClaimConstants.UserRoleInTenantClaimType);
        }

        public static string GetClaimValue(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            try
            {
                return claimsPrincipal.FindFirst(claimType)?.Value;
            }
            catch(Exception _)
            {
                throw new ClaimNotFoundException();
            }
        }
    }
}
