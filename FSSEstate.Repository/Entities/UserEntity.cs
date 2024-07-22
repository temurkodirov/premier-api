using FSSEstate.Core.Enums;

namespace FSSEstate.Repository.Entities
{
    public class UserEntity : Auditable
    {
        public string Email { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public SpecialityType SpecialityType { get; set; }
        public string Province { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string AboutMe { get; set; } = string.Empty;
        public string FacebookUrl { get; set; } = string.Empty;
        public string GooglePlusUrl { get; set; } = string.Empty;
        public string TwitterUrl { get; set; } = string.Empty;
        public string LinkedInUrl { get; set; } = string.Empty;
        public string? ProfileImage { get; set; }
        public UserStatus Status { get; set; }
        public long AccountId { get; set; }
    }
}
