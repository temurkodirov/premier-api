using FSSEstate.Core.Enums;

namespace FSSEstate.Core.Models.UserModels
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? ProfileImage { get; set; }
        public string EmailOrPhoneNumber { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public bool CompanyOwner { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public UserStatus Status { get; set; }
    }
}
