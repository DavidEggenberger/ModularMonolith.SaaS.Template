using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Server.BuildingBlocks.HostingInformation
{
    public static class ServerInformationDIRegistrator
    {
        public static IServiceCollection RegisterServerInformationProvider(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IServerInformationProvider, ServerInformationProvider>();
        }
    }
}
