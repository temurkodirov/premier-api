using FSSEstate.Core.Utility.Listing;
using System.ComponentModel.DataAnnotations;

namespace FSSEstate.Core.Models.InformationPhotosModels
{
    public class InformationPhotoFilterParams : QueryStringParameters
    {
        [Required]
        public long InformationId { get; set; }
    }
}
