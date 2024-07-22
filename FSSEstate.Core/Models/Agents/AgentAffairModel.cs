using FSSEstate.Core.Models.AffairModels;

namespace FSSEstate.Core.Models.Agents
{
    public class AgentAffairModel
    {
        public long Id { get; set; }
        public long AgentId { get; set; }
        public AgentModel? Agent { get; set; }
        public long SubAffairId { get; set; }
        public AffairModel? Affair { get; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
    }
}
