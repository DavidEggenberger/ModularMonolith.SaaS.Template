using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            #if ( EnableAzureKeyVault == True )
                .ConfigureAppConfiguration((hostingcontext, config) =>
                {
                    config.AddAzureKeyVault(new Uri(""),
                        new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = "" }));
                })
            #endif
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
