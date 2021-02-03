namespace BlazorServerConfiguration.Models
{
    public class StockRequest
    {
        public string Symbol { get; set; } = null!;
        public string Region { get; set; } = null!;
    }

    public enum RegionCode
    {
        US,
        BR,
        AU,
        CA,
        FR,
        DE,
        HK,
        IN,
        IT,
        ES,
        GB,
        SG,
    }
}
