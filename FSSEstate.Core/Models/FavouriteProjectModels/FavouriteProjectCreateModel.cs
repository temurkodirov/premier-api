namespace FSSEstate.Core.Models.FavouriteProjectModels
{
    public class FavouriteProjectCreateModel
    {
        public long AccountId { get; set; }
        public long ProjectId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5);
    }
}
