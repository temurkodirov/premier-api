using FSSEstate.Core.Enums;

namespace FSSEstate.Repository.Entities;

public class AccountEntity : Auditable
{
    public string EmailOrPhoneNumber { get; set; } = string.Empty;
    public bool IsVerified { get; set; } = false;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
}
