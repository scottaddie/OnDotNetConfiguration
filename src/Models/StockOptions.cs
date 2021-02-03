namespace BlazorServerConfiguration.Models
{
    public class StockOptions
    {
        public string ApiKey { get; set; } = null!;
        public string Endpoint { get; set; } = null!;
        public string HostName { get; set; } = null!;
        public string RegionCode { get; set; } = null!;
        public string TickerSymbol { get; set; } = null!;
    }
}
