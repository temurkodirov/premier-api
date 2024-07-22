namespace FSSEstate.Core.Models.xProductImageModels;

public class xProductImageModel
{
    public long Id { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public long ProductId { get; set; }
    public bool IsMain { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
