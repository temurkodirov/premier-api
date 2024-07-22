namespace FSSEstate.Core.Models.ProjectPhotosModels
{
    public class ProjectPhotoCreateModel
    {
        public string ImagePath { get; set; } = string.Empty;
        public long ProjectId { get; set; }
        public bool IsMain { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5);
    }
}
