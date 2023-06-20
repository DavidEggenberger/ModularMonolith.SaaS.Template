using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kernel.BuildingBlocks;

namespace Web.Server.BuildingBlocks.HostingInformation
{
    public static class WebContextAccessorDIRegistrator
    {
        public static IServiceCollection RegisterWebContextAccessor(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSingleton<IWebContextAccessor, WebContextAccessor>();
        }
    }
}
