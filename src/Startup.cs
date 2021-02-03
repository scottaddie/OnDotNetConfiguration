using BlazorServerConfiguration.Models;
using BlazorServerConfiguration.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlazorServerConfiguration
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => 
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<StockOptions>(Configuration.GetSection(nameof(StockOptions)));
            //TODO: demonstrate validation by deleting one of the required properties from appsettings.json
            services.AddOptions<StockOptions>()
                    .Bind(Configuration.GetSection(nameof(StockOptions)))
                    .ValidateDataAnnotations();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSignalR()
                    .AddAzureSignalR();
            services.AddMemoryCache();
            services.AddHttpClient();
            services.AddSingleton<WeatherService>();
            services.AddSingleton<StockService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
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
    }
}
