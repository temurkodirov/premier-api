namespace FSSEstate.Repository.Entities;

public class AgentAffairEntity : Auditable
{
    public long AgentId { get; set; }
    public AgentEntity? Agent { get; set; }
    public long AffairId { get; set; }
    public AffairEntity? Affair { get;}
}
