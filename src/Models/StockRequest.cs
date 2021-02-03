namespace BlazorServerConfiguration.Models
{
    public class StockRequest
    {
        public string Symbol { get; init; } = null!;
        public RegionCode Region { get; init; }
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
