using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Authentication.AntiforgeryToken
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
