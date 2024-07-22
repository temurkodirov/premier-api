using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Core.Models.Agents
{
    public class AgentAffairFilterParams : QueryStringParameters
    {
        public string SearchText { get; set; } = string.Empty;
        public long? AffairId { get; set; }
    }
}
