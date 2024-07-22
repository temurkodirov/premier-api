using Microsoft.AspNetCore.Http;

namespace FSSEstate.Core.Models.InformationModels;

public class InformationCreateModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long AccountId { get; set; }
    public string SeoUrl { get; set; } = string.Empty;
    public List<IFormFile> Images { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5);
}
