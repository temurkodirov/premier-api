using FSSEstate.Core.Enums;

namespace FSSEstate.Core.Models.AffairModels
{
    public class AffairModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameUz { get; set; } = string.Empty;
        public string NameRu { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long? ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
