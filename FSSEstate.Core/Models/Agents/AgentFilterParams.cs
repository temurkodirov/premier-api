using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Core.Models.Agents
{
    public class AgentFilterParams : QueryStringParameters
    {
        public string SearchText { get; set; } = string.Empty;
    }
}
