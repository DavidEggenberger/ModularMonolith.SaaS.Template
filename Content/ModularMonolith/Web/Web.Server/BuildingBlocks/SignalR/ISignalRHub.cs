using Microsoft.AspNetCore.SignalR;

namespace Web.Server.BuildingBlocks.SignalR
{
    public interface ISignalRHub
    {
        public IHubCallerClients Clients { get; set; }
        public HubCallerContext Context { get; set; }
        public IGroupManager Groups { get; set; }
    }
}
