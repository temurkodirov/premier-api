using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Core.Models.FavouriteProjectModels
{
    public class FavouriteProjectFilterParams : QueryStringParameters
    {
        public string SearchText { get; set; } = string.Empty;
        public long? CategoryId { get; set; }
    }
}
