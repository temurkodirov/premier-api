namespace FSSEstate.API.Models.ProjectCategoryModels
{
    public class CategoryResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameUz { get; set; } = string.Empty;
        public string NameRu { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string Code { get; set; }
        public long? ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
