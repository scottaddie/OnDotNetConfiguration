using System.ComponentModel.DataAnnotations;

namespace BlazorServerConfiguration.Models
{
    public class QuoteOptions
    {
        [Required]
        public string ApiKey { get; init; } = null!;

        [Range(0, 1000)]
        public double CacheDurationInMinutes { get; init; }

        [Required]
        public string CurrencyCode { get; init; } = null!;

        [Required]
        public string Endpoint { get; init; } = null!;

        [Required]
        public string HostName { get; init; } = null!;

        public CryptoQuote? CryptoQuote { get; set; }

        public StockQuote? StockQuote { get; set; }
    }
}
