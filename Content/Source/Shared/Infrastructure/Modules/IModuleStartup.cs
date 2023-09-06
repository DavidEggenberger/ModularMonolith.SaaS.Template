using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace Shared.Infrastructure.Modules
{
    public interface IModuleStartup
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration = null);
        void Configure(IApplicationBuilder app, IHostEnvironment env);
    }
}
