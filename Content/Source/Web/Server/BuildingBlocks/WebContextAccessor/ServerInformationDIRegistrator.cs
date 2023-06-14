using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks;

namespace Web.Server.BuildingBlocks.HostingInformation
{
    public static class ServerInformationDIRegistrator
    {
        public static IServiceCollection RegisterServerInformationProvider(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IWebContextAccessor, ServerInformationProvider>();
        }
    }
}
