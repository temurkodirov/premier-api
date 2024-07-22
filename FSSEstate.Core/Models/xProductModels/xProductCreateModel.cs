using FSSEstate.Core.Models.xProductCharacteristicsModels;
using Microsoft.AspNetCore.Http;

namespace FSSEstate.Core.Models.xProductModels;

public class xProductCreateModel
{
    public string Name { get; set; } = string.Empty;
    public long? CategoryId { get; set; }
    public string? Description { get; set; } = string.Empty;
    public decimal? Price { get; set; } = decimal.Zero;
    public string? Model { get; set; } = string.Empty;
    public string? Volt { get; set; } = string.Empty;
    public List<xProductCharacteristicsCreateModel> Characteristics { get; set; } = default;
    public string? Quvvati { get; set; } = string.Empty;
    public string? Material { get; set; } = string.Empty;
    public string? Speed { get; set; } = string.Empty;
    public string? Size { get; set; } = string.Empty;
    public decimal? Weight { get; set; } = decimal.Zero;
    public List<IFormFile> Images { get; set; } = default!;
}
