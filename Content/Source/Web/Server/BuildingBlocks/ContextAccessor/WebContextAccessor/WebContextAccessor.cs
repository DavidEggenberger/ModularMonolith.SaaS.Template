using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks;
using System;
using System.Linq;

namespace Web.Server.BuildingBlocks.ContextAccessor.WebContextAccessor
{
    public class WebContextAccessor : IWebContextAccessor
    {
        public Uri BaseURI { get; set; }
        public WebContextAccessor(IServiceProvider serviceProvider)
        {
            var server = serviceProvider.GetService<IServer>();

            var addresses = server?.Features.Get<IServerAddressesFeature>();
            
            BaseURI = new Uri(addresses?.Addresses.FirstOrDefault(a => a.Contains("https")) ?? string.Empty);
        }
    }
}
