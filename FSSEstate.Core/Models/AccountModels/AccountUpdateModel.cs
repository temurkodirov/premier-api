using FSSEstate.Core.Enums;

namespace FSSEstate.Core.Models.AccountModels
{
    public class AccountUpdateModel
    {
        public long Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
    }
}
