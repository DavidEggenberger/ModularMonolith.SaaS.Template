using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.Constants;

namespace Shared.Features.SignalR
{
    public static class Registrator
    {
        public static IServiceCollection Add_SignalR(this IServiceCollection services)
        {
            services.AddSingleton<IUserIdProvider, UserIdProvider>();
            services.AddScoped<NotificationHubService>();
            return services;
        }

        public static IApplicationBuilder UseSignalRMiddleware(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationHub>(NotificationHubConstants.Hub);
            });

            return app;
        }
    }
}
