namespace FSSEstate.Repository.Entities
{
    public class ProjectPhotosEntity : Auditable
    {
        public string ImagePath { get; set; } = string.Empty;
        public bool IsMain { get; set; } = false;
        public long ProjectId { get; set; }
    }
}
    