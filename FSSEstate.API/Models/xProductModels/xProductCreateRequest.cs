using FSSEstate.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FSSEstate.API.Models.xProductModels;

public class xProductCreateRequest
{
    
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

    [ModelBinder(BinderType = typeof(JsonModelBinder))]
    public List<xProductCharacterRequestModel> CharacteristicsList { get; set; }
   public List<IFormFile> Images { get; set; } = default!;
}
