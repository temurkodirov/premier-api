namespace FSSEstate.Repository.Entities;

public class xProduct : Auditable
{
    public string Name { get; set; } = string.Empty;
    public long? CategoryId { get; set; }
    public CategoryEntity? Category { get; set; }
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
}
