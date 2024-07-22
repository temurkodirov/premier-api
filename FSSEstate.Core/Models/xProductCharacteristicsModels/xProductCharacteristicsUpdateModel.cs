namespace FSSEstate.Core.Models.xProductCharacteristicsModels;

public class xProductCharacteristicsUpdateModel
{
    public long Id { get; set; }
    public string? NameUz { get; set; } = string.Empty;
    public string? DescUz { get; set; } = string.Empty;
    public string? NameRu { get; set; } = string.Empty;
    public string? DescRu { get; set; } = string.Empty;
    public long? ProductId { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.AddHours(5);

}
