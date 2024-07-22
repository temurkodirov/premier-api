using FSSEstate.Core.Models.AccountModels;
using FSSEstate.Repository.Entities;

namespace FSSEstate.Business.Interfaces
{
    public interface IAccountService
    {
        public Task<bool> CreateAsync(AccountCreateModel account);
        public Task<string> VerifyAccountAsync(string emailOrPhoneNumber, string code);
        public Task<bool> DeleteAsync(long id);
        public Task<AccountEntity> GetByIdAsync(long id);
        public Task<AccountEntity> GetByEmailOrPhoneNumAsync(string EmailOrPhoneNumber);
        public Task<string> UpdatePassword(PasswordUpdateModel passwordUpdateModel, string id);
        public Task<bool> UpdateAsync(AccountUpdateModel account);
    }
}
