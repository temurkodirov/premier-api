namespace FSSEstate.Repository.Entities
{
    public class InformationPhotosEntity : Auditable
    {
        public string ImagePath { get; set; } = string.Empty;
        public bool IsMain { get; set; } = false;
        public long InformationId { get; set; }

    }
}
