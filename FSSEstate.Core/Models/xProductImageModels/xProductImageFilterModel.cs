using FSSEstate.Core.Utility.Listing;
using System.ComponentModel.DataAnnotations;

namespace FSSEstate.Core.Models.xProductImageModels;

public class xProductImageFilterModel : QueryStringParameters
{
    [Required]
    public long xProductId { get; set; }
}
