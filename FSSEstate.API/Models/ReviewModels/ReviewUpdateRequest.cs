namespace FSSEstate.API.Models.ReviewModels
{
    public class ReviewUpdateRequest
    {
        public long Id { get; set; }
        public decimal Mark { get; set; }
        public string Description { get; set; } = string.Empty;
        public long? ProjectId { get; set; }
        public string UserAlias { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
    }
}
