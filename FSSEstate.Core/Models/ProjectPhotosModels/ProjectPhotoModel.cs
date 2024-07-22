namespace FSSEstate.Core.Models.ProjectPhotosModels
{
    public class ProjectPhotoModel
    {
        public long Id { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public long ProjectId { get; set; }
        public bool IsMain { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
