using BlazorServerConfiguration.Models;
using BlazorServerConfiguration.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

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
            services.AddOptions<QuoteOptions>()
                    .Bind(Configuration.GetSection(nameof(QuoteOptions)))
                    .ValidateDataAnnotations();
            services.AddRazorPages();
            services.AddServerSideBlazor();

            if (HostEnvironment.IsProduction())
            {
                services.AddSignalR()
                        .AddAzureSignalR(Configuration["Azure:SignalR:ConnectionString"]);
            }

            services.AddAzureAppConfiguration();
            services.AddFeatureManagement();
            services.AddMemoryCache();
            services.AddHttpClient();
            services.AddSingleton<QuoteService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (HostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseAzureAppConfiguration();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                if (HostEnvironment.IsDevelopment())
                {
                    endpoints.MapGet("config", async context =>
                    {
                        var configInfo = (Configuration as IConfigurationRoot).GetDebugView();
                        await context.Response.WriteAsync(configInfo);
                    });
                }

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
