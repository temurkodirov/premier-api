namespace FSSEstate.Repository.Entities;

public class ReviewEntity : Auditable
{
    public decimal Mark { get; set; }
    public string Description { get; set; } = string.Empty;
    public AccountEntity? Account { get; set; }
    public long? AccountId { get; set; }
    public ProjectEntity? Project { get; set; }
    public long? ProjectId { get; set; }
    public string UserAlias { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
}
