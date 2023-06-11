using System;

namespace Web.Server.BuildingBlocks.HostingInformation
{
    public interface IServerInformationProvider
    {
        public Uri BaseURI { get; set; }
    }
}
