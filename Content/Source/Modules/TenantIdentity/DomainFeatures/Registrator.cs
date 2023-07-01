using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks.Authorization.Constants;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Modules.TenantIdentity.DomainFeatures.UserAggregate.Domain;
using Modules.TenantIdentity.DomainFeatures.Infrastructure;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.EFCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Modules.TenantIdentity.DomainFeatures.Infrastructure.Configuration;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;

namespace Modules.TenantIdentity.DomainFeatures
{
    public static class Registrator
    {
        public static IServiceCollection RegisterTenantIdentityModule(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {                
            services.AddSingleton<OpenIdConnectPostConfigureOptions>();
            services.AddScoped<ContextUserClaimsPrincipalFactory<User>>();

            services.RegisterConfiguration(configuration);

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromSeconds(0);
            });

            services.AddDbContext<TenantIdentityDbContext>();

            var tenantIdentityConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<TenantIdentityConfiguration>>().Value;

            AuthenticationBuilder authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
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

            return services;
        }
    }
}
