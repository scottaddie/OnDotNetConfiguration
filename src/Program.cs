using BlazorServerConfiguration.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration.Json;
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
                .ConfigureAppConfiguration((context, config) =>
                {
                    if (context.HostingEnvironment.IsProduction())
                        config.ConfigureKeyVault();

                    Console.WriteLine("Configuration sources\n=====================");
                    foreach (var source in config.Sources)
                    {
                        if (source is JsonConfigurationSource jsonSource)
                            Console.WriteLine($"{source}: {jsonSource.Path}");
                        else
                            Console.WriteLine(source.ToString());
                    }
                    Console.WriteLine("=====================\n");
                })
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
