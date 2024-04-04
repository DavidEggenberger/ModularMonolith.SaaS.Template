using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Shared.Features.Modules
{
    public interface IModuleStartup
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration = null);
        void Configure(IApplicationBuilder app, IHostEnvironment env);
    }
}
