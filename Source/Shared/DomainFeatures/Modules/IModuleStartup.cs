﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Shared.DomainFeatures.Modules
{
    public interface IModuleStartup
    {
        public Assembly? DomainFeaturesAssembly { get; }
        void ConfigureServices(IServiceCollection services, IConfiguration configuration = null);
        void Configure(IApplicationBuilder app, IHostEnvironment env);
    }
}
