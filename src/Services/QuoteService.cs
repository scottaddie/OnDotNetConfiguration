using BlazorServerConfiguration.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorServerConfiguration.Services
{
    public class QuoteService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly QuoteOptions _quoteOptions;

        public QuoteService(
            HttpClient httpClient,
            IMemoryCache cache,
            IOptions<QuoteOptions> quoteConfiguration)
        {
            _httpClient = httpClient;
            _cache = cache;

            _quoteOptions = quoteConfiguration.Value;
            _httpClient.BaseAddress = new(_quoteOptions.Endpoint);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _quoteOptions.HostName);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _quoteOptions.ApiKey);
        }

        public async ValueTask<QuoteStats> GetStatisticsAsync(QuoteRequest request)
        {
            var cacheKey = $"{request.Symbol}_{request.Region}";

            if (!_cache.TryGetValue(cacheKey, out QuoteStats? stats))
            {
                // key not in cache, so fetch data

                var queryString = $"?symbol={request.Symbol}&region={request.Region}";
                var response = await _httpClient.GetAsync(queryString);

                response.EnsureSuccessStatusCode();
                stats = await response.Content.ReadFromJsonAsync<QuoteStats>();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(_quoteOptions.CacheDurationInMinutes)
                };

                // save data to cache
                _cache.Set(cacheKey, stats, cacheEntryOptions);
            }

            return stats!;
        }
    }
}
