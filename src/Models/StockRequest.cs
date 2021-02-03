using System.ComponentModel.DataAnnotations;

namespace BlazorServerConfiguration.Models
{
    public record StockRequest(
        [Required] string Symbol,
        RegionCode Region
    );

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
