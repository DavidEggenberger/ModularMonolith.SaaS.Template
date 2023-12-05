using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;
using Web.Client.BuildingBlocks;

namespace Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RegisterBuildingBlocks();

            await builder.Build().RunAsync();
        }
    }
}
