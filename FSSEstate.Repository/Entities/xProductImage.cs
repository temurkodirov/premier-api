namespace FSSEstate.Repository.Entities;

public class xProductImage : Auditable
{
    public string ImagePath { get; set; } = string.Empty;
    public bool IsMain { get; set; } = false;
    public long ProductId { get; set; }
}
