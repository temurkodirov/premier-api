namespace FSSEstate.Repository.Entities
{
    public class SubCategoryEntity : Auditable
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public long ParentId { get; set; }
        public CategoryEntity? Parent { get; set; }
    }
}
