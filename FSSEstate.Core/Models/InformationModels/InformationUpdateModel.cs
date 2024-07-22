namespace FSSEstate.Core.Models.InformationModels
{
    public class InformationUpdateModel
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long AccountId { get; set; }
        public string SeoUrl { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.AddHours(5);
    }
}
