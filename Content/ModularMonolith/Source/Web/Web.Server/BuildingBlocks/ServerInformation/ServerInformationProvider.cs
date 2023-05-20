using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Web.Server.BuildingBlocks.HostingInformation
{
    public class ServerInformationProvider : IServerInformationProvider
    {
        public Uri BaseURI { get; set; }
        public ServerInformationProvider(IServiceProvider serviceProvider)
        {
            var server = serviceProvider.GetService<IServer>();

            var addresses = server?.Features.Get<IServerAddressesFeature>();

            BaseURI = new Uri(addresses?.Addresses.FirstOrDefault(a => a.Contains("https")) ?? string.Empty);
        }
    }
}
