using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingcontext, config) =>
                {
                    //#if (EnableAzureKeyVault)
                    config.AddAzureKeyVault(new Uri("https://{yourKeyVault}.vault.azure.net/"),
                        new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = "{YourMangedIdentityClientId}" }));
                    //#endif
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
