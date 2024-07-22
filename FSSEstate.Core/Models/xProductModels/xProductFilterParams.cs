using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Core.Models.xProductModels;

public class xProductFilterParams : QueryStringParameters
{
    public string SearchText { get; set; } = string.Empty;
    public decimal? MaxPrice { get; set; }
    public decimal? MinPrice { get; set; }
    public long? CategoryId { get; set; }
}
