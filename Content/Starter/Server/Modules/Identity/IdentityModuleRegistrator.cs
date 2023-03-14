using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Server.Modules.Identity
{
    public static class IdentityModuleRegistrator
    {
        public static void RegisterIdentityModule(this IServiceCollection services, IConfiguration configuration)
        {
            AuthenticationBuilder authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = configuration["SocialLogins:Microsoft:ClientId"];
                    options.ClientSecret = configuration["SocialLogins:Microsoft:ClientSecret"];
                })
                .AddGitHub(options =>
                {
                    options.ClientId = configuration["SocialLogins:Github:ClientId"];
                    options.ClientSecret = configuration["SocialLogins:Github:ClientSecret"];
                })
                .AddGoogle(options =>
                {
                    options.ClientId = configuration["SocialLogins:Google:ClientId"];
                    options.ClientSecret = configuration["SocialLogins:Google:ClientSecret"];
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

            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration["AzureSQLConnection"], sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(5);
                });
            });
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.User.RequireUniqueEmail = false;
                options.Stores.MaxLengthForKeys = 128;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();
        }
    }
}
