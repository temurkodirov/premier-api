namespace FSSEstate.Repository.Entities;

public class Auditable : BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5);
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.AddHours(5);
}
