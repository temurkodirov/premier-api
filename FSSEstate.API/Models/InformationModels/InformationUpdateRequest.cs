namespace FSSEstate.API.Models.InformationModels
{
    public class InformationUpdateRequest
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
