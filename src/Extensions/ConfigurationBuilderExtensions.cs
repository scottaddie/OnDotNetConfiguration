using Azure.Identity;
using Microsoft.Extensions.Configuration;
using System;

namespace BlazorServerConfiguration.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static void ConfigureAzAppConfiguration(
            this IConfigurationBuilder builder,
            string? endpoint)
        {
            if (endpoint is null)
                throw new ArgumentNullException("App Configuration endpoint not provided.");

            var credential = new DefaultAzureCredential();

            builder.AddAzureAppConfiguration(options =>
                options.Connect(new(endpoint), credential)
                       .ConfigureKeyVault(vaultOptions =>
                       {
                           vaultOptions.SetCredential(credential);
                       })
                       .ConfigureRefresh(refreshOptions =>
                       {
                           refreshOptions.Register("Settings:Sentinel", refreshAll: true)
                                         .SetCacheExpiration(new(0, 0, 10));
                       })
                       .UseFeatureFlags(flagOptions =>
                       {
                           flagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(5);
                       }));
        }
    }
}
