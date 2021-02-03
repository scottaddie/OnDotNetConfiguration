using System.ComponentModel.DataAnnotations;

namespace BlazorServerConfiguration.Models
{
    public class StockOptions
    {
        [Required]
        public string ApiKey { get; set; } = null!;
        
        [Required]
        public string Endpoint { get; set; } = null!;

        [Required]
        public string HostName { get; set; } = null!;
        
        [Required]
        public string RegionCode { get; set; } = null!;

        [Required, StringLength(maximumLength: 5, MinimumLength = 1)]
        public string TickerSymbol { get; set; } = null!;
    }
}
