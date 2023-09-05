using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks.Authorization.Constants;
using System.Security.Claims;
using Modules.TenantIdentity.DomainFeatures.Infrastructure;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Modules.TenantIdentity.DomainFeatures.Aggregates.UserAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.Configuration;
using Shared.Infrastructure.EFCore;

namespace Modules.TenantIdentity.Server
{
    public class TenantIdentityModuleStartup : IModuleStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<OpenIdConnectPostConfigureOptions>();
            services.AddScoped<ContextUserClaimsPrincipalFactory<User>>();

            services.RegisterConfiguration(configuration);

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromSeconds(0);
            });

            services.AddDbContext<TenantIdentityDbContext>();

            if (webHostEnvironment.IsProduction())
            {
                services.MigrateContext<TenantIdentityDbContext>();
            }

            //var tenantIdentityConfiguration = services.BuildServiceProvider().GetRequiredService<TenantIdentityConfiguration>();

            AuthenticationBuilder authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = AuthConstant.ApplicationAuthenticationScheme;
                options.DefaultSignInScheme = AuthConstant.ApplicationAuthenticationScheme;
            })
                .AddLinkedIn(options =>
                {
                    options.ClientId = "dadsfadsf";
                    options.ClientSecret = "dadsfadsf";
                })
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = "dadsfadsf";
                    options.ClientSecret = "dadsfadsf";
                })
                .AddGoogle(options =>
                {
                    options.ClientId = "dadsfadsf";
                    options.ClientSecret = "dadsfadsf";
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
            var identityService = services.AddIdentityCore<User>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.User.RequireUniqueEmail = true;
                options.Stores.MaxLengthForKeys = 128;
                options.ClaimsIdentity.UserIdClaimType = ClaimConstants.UserIdClaimType;
                options.ClaimsIdentity.UserNameClaimType = ClaimConstants.UserNameClaimType;
            })
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<ContextUserClaimsPrincipalFactory<User>>()
                .AddEntityFrameworkStores<TenantIdentityDbContext>()
                .AddSignInManager();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            
        }
    }
}
