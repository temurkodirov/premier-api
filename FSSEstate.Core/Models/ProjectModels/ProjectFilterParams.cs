using FSSEstate.Core.Enums;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Core.Models.ProjectModels;

public class ProjectFilterParams : QueryStringParameters
{
    public string SearchText { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public ProjectStatus? Status { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? MinPrice { get; set; }
    public long? CategoryId { get; set; }
}
