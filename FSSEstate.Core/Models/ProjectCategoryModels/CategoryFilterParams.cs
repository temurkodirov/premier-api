using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Core.Models.ProjectCategoryModels
{
    public class CategoryFilterParams : QueryStringParameters
    {
        public string SearchText { get; set; } = string.Empty;
        public long? ParentId { get; set; } 
    }
}
