namespace BlazorServerConfiguration.Models
{
    public class StockStats
    {
        public QuoteType QuoteType { get; init; } = null!;

        public Price Price { get; init; } = null!;
    }

    public class QuoteType
    {
        public string LongName { get; init; } = null!;
    }

    public class Price
    {
        public string QuoteSourceName { get; init; } = null!;

        public string Currency { get; init; } = null!;

        public string CurrencySymbol { get; init; } = null!;
        
        public string MarketState { get; init; } = null!;

        public RegularMarketPrice RegularMarketPrice { get; init; } = null!;

        public RegularMarketChange RegularMarketChange { get; init; } = null!;

        public RegularMarketChangePercent RegularMarketChangePercent { get; init; } = null!;

        public override string ToString() =>
            $"{CurrencySymbol}{RegularMarketPrice.Fmt} ({Currency})";
    }

    public class RegularMarketPrice
    {
        public string Fmt { get; init; } = null!;
    }

    public class RegularMarketChange
    {
        public string Fmt { get; init; } = null!;
    }

    public class RegularMarketChangePercent
    {
        public string Fmt { get; init; } = null!;

        public bool IsLoss => Fmt.StartsWith("-");
    }
}
