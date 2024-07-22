using FSSEstate.Core.Utility.Listing;
using System.ComponentModel.DataAnnotations;

namespace FSSEstate.Core.Models.xProductCharacteristicsModels;

public class xProductCharacteristicsFilterModel : QueryStringParameters
{
    [Required]
    public long xProductId { get; set; }
}
