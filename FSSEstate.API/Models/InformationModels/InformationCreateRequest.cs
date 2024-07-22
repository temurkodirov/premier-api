namespace FSSEstate.API.Models.InformationModels
{
    public class InformationCreateRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<IFormFile> Images { get; set; } = default!;
    }
}
