using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using Web.Client.BuildingBlocks.Services.Http;

namespace Web.Client.BuildingBlocks
{
    public static class Registrator
    {
        public static void RegisterBuildingBlocks(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddHttpClient(HttpClientConstants.DefaultHttpClient, client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            //builder.Services.AddHttpClient(HttpClientConstants.AuthenticatedHttpClient, client =>
            //{
            //    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //}).AddHttpMessageHandler<AuthorizedHandler>();

            builder.Services.AddScoped<HttpClientService>();
            builder.Services.AddScoped<AuthorizedHttpClientService>();
        }
    }
}
