using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Client.BuildingBlocks.Auth;
using Shared.Client.BuildingBlocks.Auth.Antiforgery;
using Shared.Client.BuildingBlocks.Http;
using System;
using System.Net.Http.Headers;

namespace Shared.Client.BuildingBlocks
{
    public static class Registrator
    {
        public static void AddBuildingBlocks(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped<AntiforgeryTokenService>();

            builder.Services.AddTransient<AuthorizedHandler>();
            builder.Services.TryAddSingleton<AuthenticationStateProvider, HostAuthenticationStateProvider>();

            builder.Services.AddHttpClient(HttpClientConstants.DefaultHttpClient, client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            builder.Services.AddHttpClient(HttpClientConstants.AuthenticatedHttpClient, client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddHttpMessageHandler<AuthorizedHandler>();

            builder.Services.AddScoped<HttpClientService>();
            builder.Services.AddScoped<AuthorizedHttpClientService>();
        }
    }
}
