using System.ComponentModel.DataAnnotations;

namespace BlazorServerConfiguration.Models
{
    public interface IQuote
    {
        public string Symbol { get; set; }

        public RegionCode RegionCode { get; set; }
    }

    public class StockQuote : IQuote
    {
        [StringLength(maximumLength: 5, MinimumLength = 1)]
        public string Symbol { get; set; } = null!;

        public RegionCode RegionCode { get; set; }
    }

    public class CryptoQuote : IQuote
    {
        [StringLength(maximumLength: 5, MinimumLength = 1)]
        public string Symbol { get; set; } = null!;

        [Required]
        public string CurrencyCode { get; set; } = null!;

        public RegionCode RegionCode { get; set; }
    }
}
