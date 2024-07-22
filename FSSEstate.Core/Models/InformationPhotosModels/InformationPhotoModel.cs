namespace FSSEstate.Core.Models.InformationPhotosModels
{
    public class InformationPhotoModel
    {
        public long Id { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public long InformationId { get; set; }
        public bool IsMain { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
