using FSSEstate.Core.Utility.Listing;
using System.ComponentModel.DataAnnotations;

namespace FSSEstate.Core.Models.ProjectPhotosModels
{
    public class ProjectPhotoFilterParams : QueryStringParameters
    {
        [Required]
        public long ProjectId { get; set; }
    }
}
