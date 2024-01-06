using System.Security.Claims;

namespace Shared.Kernel.BuildingBlocks.Auth.Constants
{
    public class ClaimConstants
    {
        public const string UserIdClaimType = ClaimTypes.Sid;
        public const string UserNameClaimType = ClaimTypes.Name;
        public const string EmailClaimType = ClaimTypes.Email;
        public const string PictureClaimType = "picture";
        public const string TenantIdClaimType = "TenantId";
        public const string UserRoleInTenantClaimType = "UserRoleInTenant";
        public const string TenantPlanClaimType = "TenantPlan";
        public const string TenantNameClaimType = "TenantPlan";
    }
}
