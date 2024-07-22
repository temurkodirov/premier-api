namespace FSSEstate.Core.Models.InformationPhotosModels
{
    public class InformationPhotoUpdateModel
    {
        public long Id { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public long InformationId { get; set; }
        public bool IsMain { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.AddHours(5);
    }
}
