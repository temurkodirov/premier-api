namespace FSSEstate.Repository.Entities
{
    public class FavouriteProjectEntity : Auditable
    {
        public long AccountId { get; set; } 
        public AccountEntity Account { get; set; }
        public long ProjectId { get; set; }
        public ProjectEntity Project { get; set; }
    }
}
