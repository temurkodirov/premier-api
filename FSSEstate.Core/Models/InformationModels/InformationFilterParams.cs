using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Core.Models.InformationModels
{
    public class InformationFilterParams : QueryStringParameters
    {
        public string SearchText { get; set; } = string.Empty;
        public long? AccountId { get; set; }
    }
}
