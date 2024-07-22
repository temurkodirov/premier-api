using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Core.Models.ReviewModels
{
    public class ReviewFilterParams : QueryStringParameters
    {
        public string SearchText { get; set; } = string.Empty;
        public long? ProjectId { get; set; }
        public long? AccountId { get; set; }
    }
}
