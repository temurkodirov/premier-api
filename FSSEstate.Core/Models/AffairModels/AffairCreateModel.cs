using FSSEstate.Core.Enums;

namespace FSSEstate.Core.Models.AffairModels
{
    public class AffairCreateModel
    {
        public string Name { get; set; } = string.Empty;
        public string NameUz { get; set; } = string.Empty;
        public string NameRu { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long? ParentId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5);

    }
}
