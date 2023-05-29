using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Identity.Web.Client
{
    public static class IdentityModuleRegistrator
    {
        public static void RegisterIdentityModule(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddTransient<AuthorizedHandler>();

            builder.Services.TryAddSingleton<AuthenticationStateProvider, HostAuthenticationStateProvider>();
            builder.Services.AddAuthorizationCore();
        }
    }
}
