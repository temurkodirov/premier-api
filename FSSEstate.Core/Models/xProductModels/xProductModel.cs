using FSSEstate.Core.Models.ProjectCategoryModels;
using FSSEstate.Core.Models.xProductCharacteristicsModels;
using FSSEstate.Core.Models.xProductImageModels;


namespace FSSEstate.Core.Models.xProductModels;

public class xProductModel
{
    public long Id { get; set; }
    public string NameUz { get; set; } = string.Empty;
    public string NameRu { get; set; } = string.Empty;
    public long? CategoryId { get; set; }
    public string? DescriptionUz { get; set; } = string.Empty;
    public string? DescriptionRu { get; set; } = string.Empty;
    public decimal? PriceSum { get; set; } = decimal.Zero;
    public decimal? PriceUsd { get; set; } = decimal.Zero;
    public string? SeoUrl { get; set; } = string.Empty;
    public string? ItemOneUz { get; set; } = string.Empty;
    public string? ItemOneRu { get; set; } = string.Empty;
    public string? ItemTwoUz { get; set; } = string.Empty;
    public string? ItemTwoRu { get; set; } = string.Empty;
    public string? ItemThreeUz { get; set; } = string.Empty;
    public string? ItemThreeRu { get; set; } = string.Empty;
    public List<xProductImageModel>? Images { get; set; } = default!;
    public List<xProductCharacteristicsModel>? Characteristics { get; set;} = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
