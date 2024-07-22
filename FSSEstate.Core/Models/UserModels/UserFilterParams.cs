using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Core.Models.UserModels
{
    public class UserFilterParams : QueryStringParameters
    {
        public string SearchText { get; set; } = string.Empty;
    }
}
