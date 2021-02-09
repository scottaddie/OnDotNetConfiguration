using BlazorServerConfiguration.Models;
using BlazorServerConfiguration.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text;

namespace BlazorServerConfiguration
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) =>
            (Configuration, HostEnvironment) = (configuration, env);

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<StockOptions>(Configuration.GetSection(nameof(StockOptions)));
            //TODO: demonstrate validation by deleting one of the required properties from appsettings.json
            services.AddOptions<StockOptions>()
                    .Bind(Configuration.GetSection(nameof(StockOptions)))
                    .ValidateDataAnnotations();
            services.AddRazorPages();
            services.AddServerSideBlazor();

            if (HostEnvironment.IsProduction())
            {
                services.AddSignalR()
                        .AddAzureSignalR(Configuration["Azure:SignalR:ConnectionString"]);
            }

            services.AddMemoryCache();
            services.AddHttpClient();
            services.AddSingleton<StockService>();
        }

        public void Configure(
            IApplicationBuilder app,
            ILogger<Startup> logger)
        {
            if (HostEnvironment.IsDevelopment())
            {
                LogConfigurationKeys(logger);
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private void LogConfigurationKeys(ILogger<Startup> logger)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("Configuration keys");
            sb.AppendLine("==================");
            foreach (var (key, _) in Configuration.AsEnumerable().OrderBy(c => c.Key))
            {
                sb.AppendLine(key);
            }
            sb.AppendLine("==================");
            logger.LogInformation(sb.ToString());
        }
    }
}
