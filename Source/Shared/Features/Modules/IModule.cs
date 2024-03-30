using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Shared.Features.Modules
{
    public interface IModule
    {
        Assembly? FeaturesAssembly { get; }
        void ConfigureServices(IServiceCollection services, IConfiguration configuration = null);
        void Configure(IApplicationBuilder app, IHostEnvironment env);
    }
}
