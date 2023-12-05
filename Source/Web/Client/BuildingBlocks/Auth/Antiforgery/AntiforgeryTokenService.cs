﻿using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Web.Client.BuildingBlocks.Auth.Antiforgery
{
    public class AntiforgeryTokenService
    {
        private readonly IJSRuntime jSRuntime;
        public AntiforgeryTokenService(IJSRuntime jsRuntime)
        {
            this.jSRuntime = jsRuntime;
        }
        public async Task<string> GetAntiforgeryTokenAsync()
        {
            return await jSRuntime.InvokeAsync<string>("getAntiForgeryToken");
        }
    }
}
