using BlazorServerConfiguration.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace BlazorServerConfiguration
{
    public class Program
    {
        static Task Main(string[] args) =>
            CreateHostBuilder(args).Build().RunAsync();

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((_, config) =>
                {
                    config.ConfigureAzAppConfiguration(
                        Environment.GetEnvironmentVariable("AZURE_APPCONFIG_ENDPOINT"));
                })
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
