using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http.Headers;
using Web.Client.BuildingBlocks.Auth;
using Web.Client.BuildingBlocks.Auth.Antiforgery;
using Web.Client.BuildingBlocks.Http;

namespace Web.Client.BuildingBlocks
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
