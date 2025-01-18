using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;
using Shared.Kernel.BuildingBlocks;
using Shared.Client.BuildingBlocks;

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
