using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WebServer.Modules.ModelValidation
{
    public static class AntiforgeryTokenDIRegistrator
    {
        public static IServiceCollection RegisterAntiforgeryToken(this IServiceCollection services)
        {
            return services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
                options.Cookie.Name = "__Host-X-XSRF-TOKEN";
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
        }
    }
}
