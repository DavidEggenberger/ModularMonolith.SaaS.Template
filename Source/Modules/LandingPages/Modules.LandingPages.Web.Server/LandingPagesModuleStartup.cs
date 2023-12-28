﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Features.Modules;
using System.Reflection;

namespace Modules.LandingPages.Web.Server
{
    public class LandingPagesModuleStartup : IModuleStartup
    {
        public Assembly FeaturesAssembly => null;

        public void ConfigureServices(IServiceCollection services, IConfiguration config = null)
        {
            services.AddRazorPages().AddApplicationPart(typeof(LandingPagesModuleStartup).Assembly);
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

        }
    }
}
