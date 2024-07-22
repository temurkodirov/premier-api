using FSSEstate.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace FSSEstate.API.Models.ProjectCategoryModels
{
    public class CategoryCreateRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string NameUz { get; set; } = string.Empty;
        public string NameRu { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;

        [Required]
        [MinLength(5)]
        public string Code { get; set; } = string.Empty;

        public long? ParentId { get; set; }
        public int MyOrder { get; set; }

        public IFormFile? Image { get; set; }
    }
}
