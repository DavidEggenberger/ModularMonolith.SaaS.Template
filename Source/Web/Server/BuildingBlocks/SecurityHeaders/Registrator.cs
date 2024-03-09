using Microsoft.AspNetCore.Builder;

namespace Web.Server.BuildingBlocks.SecurityHeaders
{
    public static class Registrator
    {
        public static IApplicationBuilder UseSecurityHeadersMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.Use((context, next) =>
            {
                context.Response.Headers.Remove("X-Powered-By");
                context.Response.Headers.Add("X-Xss-Protection", "1");
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                //context.Response.Headers.Add(
                //        "Content-Security-Policy",
                //        "default-src 'self'; " +
                //        "img-src 'self' myblobacc.blob.core.windows.net; " +
                //        "font-src 'self'; " +
                //        "style-src 'self'; " +
                //        "script-src 'self' 'nonce-KIBdfgEKjb34ueiw567bfkshbvfi4KhtIUE3IWF' " +
                //        " 'nonce-rewgljnOIBU3iu2btli4tbllwwe'; " +
                //        "frame-src 'self';" +
                //        "connect-src 'self';");
                return next();
            });
        }
    }
}
