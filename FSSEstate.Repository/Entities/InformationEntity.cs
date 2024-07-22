namespace FSSEstate.Repository.Entities
{
    public class InformationEntity : Auditable
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long AccountId { get; set; }
        public AccountEntity Account { get; set; }
        public string SeoUrl { get; set; } = string.Empty; 
    }
}
