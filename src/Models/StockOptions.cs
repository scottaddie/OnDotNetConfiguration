using System.ComponentModel.DataAnnotations;

namespace BlazorServerConfiguration.Models
{
    public class StockOptions
    {
        [Required]
        public string ApiKey { get; init; } = null!;

        [Range(0, 1000)]
        public double CacheDurationInMinutes { get; init; }

        [Required]
        public string Endpoint { get; init; } = null!;

        [Required]
        public string HostName { get; init; } = null!;
        
        public RegionCode RegionCode { get; init; }

        [Required, StringLength(maximumLength: 5, MinimumLength = 1)]
        public string TickerSymbol { get; init; } = null!;
    }
}
