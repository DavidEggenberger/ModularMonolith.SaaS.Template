using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;
using Web.Client.BuildingBlocks;
using Shared.Kernel.BuildingBlocks;

namespace Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSharedKernel();
            builder.AddBuildingBlocks();

            await builder.Build().RunAsync();
        }
    }
}
