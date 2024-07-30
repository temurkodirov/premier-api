namespace FSSEstate.Repository.Entities;

public class xProductCharacteristics : Auditable
{
    public string? NameUz { get; set; } = string.Empty;
    public string? DescUz { get; set; } = string.Empty;
    public string? NameRu { get; set; } = string.Empty;
    public string? DescRu { get; set; } = string.Empty;
    public long ProductId {  get; set; }
    public xProduct Product { get; set; }
}
