using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Server.BuildingBlocks.AntiforgeryToken
{
    public static class Registrator
    {
        public static IServiceCollection AddAntiforgeryToken(this IServiceCollection services)
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
