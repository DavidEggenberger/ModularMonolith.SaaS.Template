using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks.Auth.Constants;

namespace Shared.Kernel.BuildingBlocks.Auth
{
    public static class Registrator
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddAuthorizationCore(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.AddPolicy(PolicyConstants.TenantMemberPolicy, options =>
                {
                    options.RequireClaim(ClaimConstants.TenantIdClaimType);
                    options.RequireRole(TenantRoleConstants.User, TenantRoleConstants.Admin);
                });
                options.AddPolicy(PolicyConstants.TenantAdminPolicy, options =>
                {
                    options.RequireClaim(ClaimConstants.TenantIdClaimType);
                    options.RequireRole(TenantRoleConstants.Admin);
                });
                options.AddPolicy(PolicyConstants.ProfessionalSubscriptionPlanPolicy, options =>
                {
                    options.RequireClaim(ClaimConstants.TenantPlanClaimType, SubscriptionPlanConstants.ProfessionalPlan);
                });
                options.AddPolicy(PolicyConstants.EnterpriseSubscriptionPlanPolicy, options =>
                {
                    options.RequireClaim(ClaimConstants.TenantPlanClaimType, SubscriptionPlanConstants.EnterprisePlan);
                });
            });

            return services;
        }
    }
}
