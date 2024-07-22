using FSSEstate.Core.Enums;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Core.Models.AffairModels
{
    public class AffairFilterParams : QueryStringParameters
    {
        public string SearchText { get; set; } = string.Empty;
    }
}
