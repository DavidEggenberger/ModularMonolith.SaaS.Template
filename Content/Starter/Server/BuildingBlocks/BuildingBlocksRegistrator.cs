using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Server.BuildingBlocks.Controllers.Swagger;
using Server.BuildingBlocks.SignalR;

namespace Server.BuildingBlocks
{
    public static class BuildingBlocksRegistrator
    {
        public static void RegisterBuildingBlocks(this IServiceCollection services)
        {
            services.AddRazorPages(options =>
            {
                options.RootDirectory = "/BuildingBlocks/Pages";
            });
            services.AddControllers();
            services.AddSingleton<IUserIdProvider, UserIdProvider>();
            services.AddSignalR();

            services.RegisterSwagger();
        }
    }
}
