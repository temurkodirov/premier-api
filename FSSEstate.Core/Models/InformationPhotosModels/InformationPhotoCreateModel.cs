namespace FSSEstate.Core.Models.InformationPhotosModels
{
    public class InformationPhotoCreateModel
    {
        public string ImagePath { get; set; } = string.Empty;
        public long InformationId { get; set; }
        public bool IsMain { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5);
    }
}
