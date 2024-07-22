namespace FSSEstate.Repository.Entities;

public class CategoryEntity : Auditable
{
    public string Name { get; set; } = string.Empty;
    public string NameUz { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public long? ParentId { get; set; }
    public string? Image { get; set; }
    public int MyOrder { get; set; }
}
