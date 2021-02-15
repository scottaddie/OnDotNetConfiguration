using BlazorServerConfiguration.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorServerConfiguration.Services
{
    public class StockService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly StockOptions _stockOptions;

        public StockService(
            HttpClient httpClient,
            IMemoryCache cache,
            IOptions<StockOptions> stockConfiguration)
        {
            _httpClient = httpClient;
            _cache = cache;

            _stockOptions = stockConfiguration.Value;
            _httpClient.BaseAddress = new(_stockOptions.Endpoint);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _stockOptions.HostName);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _stockOptions.ApiKey);
        }

        public async ValueTask<StockStats> GetStatisticsAsync(StockRequest request)
        {
            var cacheKey = $"{request.Symbol}_{request.Region}";

            if (!_cache.TryGetValue(cacheKey, out StockStats? stats))
            {
                // key not in cache, so fetch data

                var queryString = $"?symbol={request.Symbol}&region={request.Region}";
                var response = await _httpClient.GetAsync(queryString);

                response.EnsureSuccessStatusCode();
                stats = await response.Content.ReadFromJsonAsync<StockStats>();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(_stockOptions.CacheDurationInMinutes)
                };

                // save data to cache
                _cache.Set(cacheKey, stats, cacheEntryOptions);
            }

            return stats!;
        }
    }
}
