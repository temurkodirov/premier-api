using FSSEstate.Core.Models.xProductCharacteristicsModels;
using FSSEstate.Core.Models.xProductImageModels;


namespace FSSEstate.Core.Models.xProductModels;

public class xProductModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public long? CategoryId { get; set; }
    public string? Description { get; set; } = string.Empty;
    public decimal? Price { get; set; } = decimal.Zero;
    public string? Model { get; set; } = string.Empty;
    public string? SeoUrl { get; set; } = string.Empty;
    public string? Volt { get; set; } = string.Empty;
    public string? Characteristics { get; set; } = string.Empty;
    public string? Quvvati { get; set; } = string.Empty;
    public string? Material { get; set; } = string.Empty;
    public string? Speed { get; set; } = string.Empty;
    public string? Size { get; set; } = string.Empty;
    public decimal? Weight { get; set; } = decimal.Zero;
    public List<xProductImageModel> Images { get; set; } = default!;
    public List<xProductCharacteristicsModel> CharacteristicsList { get; set;} = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
