namespace FSSEstate.API.Models.xProductModels;

public class xProductCharacterRequestModel
{
    public string? NameUz { get; set; } = string.Empty;
    public string? DescUz { get; set; } = string.Empty;
    public string? NameRu { get; set; } = string.Empty;
    public string? DescRu { get; set; } = string.Empty;
    public long? ProductId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5);
}
