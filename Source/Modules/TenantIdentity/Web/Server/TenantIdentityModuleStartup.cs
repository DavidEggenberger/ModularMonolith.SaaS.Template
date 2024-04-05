using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks.Auth.Constants;
using System.Security.Claims;
using Modules.TenantIdentity.Features.Infrastructure;
using Modules.TenantIdentity.Features.Infrastructure.EFCore;
using Shared.Features.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Modules.TenantIdentity.Features.Infrastructure.Configuration;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate;
using Shared.Features.EFCore;
using Shared.Features.Modules.Configuration;

namespace Modules.TenantIdentity.Server
{
    public class TenantIdentityModuleStartup : IModuleStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddSingleton<OpenIdConnectPostConfigureOptions>();
            services.AddScoped<ContextUserClaimsPrincipalFactory<ApplicationUser>>();

            services.RegisterModuleConfiguration<TenantIdentityConfiguration, TenantIdentityConfigurationValidator>(configuration);

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromSeconds(0);
            });

            services.RegisterDbContext<TenantIdentityDbContext>();

            var tenantIdentityConfiguration = services.BuildServiceProvider().GetRequiredService<TenantIdentityConfiguration>();

            AuthenticationBuilder authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = AuthConstant.ApplicationAuthenticationScheme;
                options.DefaultSignInScheme = AuthConstant.ApplicationAuthenticationScheme;
            })
                .AddLinkedIn(options =>
                {
                    options.ClientId = tenantIdentityConfiguration.LinkedinClientId;
                    options.ClientSecret = tenantIdentityConfiguration.LinkedinClientSecret;
                })
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = tenantIdentityConfiguration.MicrosoftClientId;
                    options.ClientSecret = tenantIdentityConfiguration.MicrosoftClientSecret;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = tenantIdentityConfiguration.GoogleClientId;
                    options.ClientSecret = tenantIdentityConfiguration.GoogleClientSecret;
                    options.Scope.Add("profile");
                    options.Events.OnCreatingTicket = (context) =>
                    {
                        var picture = context.User.GetProperty("picture").GetString();
                        context.Identity.AddClaim(new Claim("picture", picture));
                        return Task.CompletedTask;
                    };
                });
            authenticationBuilder.AddIdentityCookies(options =>
            {
                options.ApplicationCookie.Configure(options =>
                {
                    options.ExpireTimeSpan = new TimeSpan(6, 0, 0);
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Name = "AuthenticationCookie";
                    options.LoginPath = "/Identity/Login";
                    options.LogoutPath = "/Identity/User/Logout";
                    options.AccessDeniedPath = "/Identity/Forbidden";
                    options.SlidingExpiration = true;
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync
                    };
                });
                options.ExternalCookie.Configure(options =>
                {
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Name = "ExternalAuthenticationCookie";
                });
                options.TwoFactorRememberMeCookie.Configure(options =>
                {
                    options.Cookie.Name = "TwoFARememberMeCookie";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                });
                options.TwoFactorUserIdCookie.Configure(options =>
                {
                    options.Cookie.Name = "TwoFAUserIdCookie";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                });
            });
            var identityService = services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.User.RequireUniqueEmail = true;
                options.Stores.MaxLengthForKeys = 128;
                options.ClaimsIdentity.UserIdClaimType = ClaimConstants.UserIdClaimType;
                options.ClaimsIdentity.UserNameClaimType = ClaimConstants.UserNameClaimType;
            })
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<ContextUserClaimsPrincipalFactory<ApplicationUser>>()
                .AddEntityFrameworkStores<TenantIdentityDbContext>()
                .AddSignInManager();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            
        }
    }
}
