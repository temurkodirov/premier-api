using FSSEstate.Core.Models.InformationPhotosModels;
using FSSEstate.Core.Models.ProjectPhotosModels;

namespace FSSEstate.Core.Models.InformationModels
{
    public class InformationModel
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long AccountId { get; set; }
        public string SeoUrl { get; set; } = string.Empty;
        public List<InformationPhotoModel> Images { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
    }
}
