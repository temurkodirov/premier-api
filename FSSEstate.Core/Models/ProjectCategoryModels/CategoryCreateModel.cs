using FSSEstate.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace FSSEstate.Core.Models.ProjectCategoryModels
{
    public class CategoryCreateModel
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string NameUz { get; set; } = string.Empty;
        public string NameRu { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public long? ParentId { get; set; }
        public IFormFile Image { get; set; }
        public int MyOrder { get; set; }
    }
}
