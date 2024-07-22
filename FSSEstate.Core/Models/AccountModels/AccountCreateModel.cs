using FSSEstate.Core.Enums;

namespace FSSEstate.Core.Models.AccountModels
{
    public class AccountCreateModel
    {
        public string EmailOrPhoneNumber { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? Fullname { get; set; }
        public AccountType AccountType { get; set; }
    }
}
