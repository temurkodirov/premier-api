using AutoMapper;
using FSSEstate.Business.Implementations.Helpers;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Business.Interfaces.Helpers;
using FSSEstate.Business.Interfaces;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces;
using FSSEstate.Core.Exceptions;
using FSSEstate.Core.Models.AccountModels;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace FSSEstate.Business.Implementations
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly ISmsHelper _smsHelper;
        private readonly IConfigurationSection _config;

        public AccountService(IUnitOfWork unitOfWork, IService services, ISmsHelper smsHelper, IJwtUtils jwtUtils, IMapper mapper, IFileService fileService)
            : base(unitOfWork, services, jwtUtils, mapper, fileService)
        {
            _smsHelper = smsHelper;
        }
        public async Task<bool> CreateAsync(AccountCreateModel obj)
        {
            try
            {
                var user = await UnitOfWork.UserRepository.GetAsync(item => item.Email.ToLower() == obj.EmailOrPhoneNumber.ToLower() || item.PhoneNumber.ToLower() == obj.EmailOrPhoneNumber.ToLower());
                if (user is not null)
                {
                    throw new AppException("User '" + obj.EmailOrPhoneNumber + "' already exists");
                }
                var accountModel =  await UnitOfWork.AccountRepository.GetAsync(item=>item.EmailOrPhoneNumber.ToLower() == obj.EmailOrPhoneNumber.ToLower());
                if (accountModel is not null)
                {
                    throw new AppException("User '" + obj.EmailOrPhoneNumber + "' already exists");
                }
                var account = Mapper.Map<AccountEntity>(obj);
                // hash password
                var resultHash = PasswordHelper.PasswordHash(obj.Password);
                account.PasswordHash = resultHash.hash;
                account.PasswordSalt = resultHash.salt;
                //  account.Code = new Random().Next(100000, 999999).ToString();

                // save account
                await UnitOfWork.AccountRepository.AddAsync(account);

                //email confirmation link
                var emailConfirmation = new ConfirmationEmailEntity();
                emailConfirmation.UpdatedAt = DateTime.UtcNow;
                emailConfirmation.Account = account;
                emailConfirmation.Guid = "1234";

                var uriBuilder = _smsHelper.CreateUrlSendToEmail(obj, "1234");

                
                await UnitOfWork.ConfirmationEmailRepository.AddAsync(emailConfirmation);

                //send url to email or phone
                if (account.EmailOrPhoneNumber.StartsWith("99"))
                    await _smsHelper.SendMessageToPhone(account.EmailOrPhoneNumber, $"Confirm the registration by clicking on the {uriBuilder}");
                else
                    await _smsHelper.SendUrlMessage(account.EmailOrPhoneNumber, $"1234");
                
                //save changes
                await UnitOfWork.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Task<bool> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountEntity> GetByEmailOrPhoneNumAsync(string EmailOrPhoneNumber)
        {
            //if (!string.IsNullOrEmpty(EmailOrPhoneNumber))
            //{
            //    var account = await UnitOfWork.AccountRepository.GetByEmailOrPhoneNumber(EmailOrPhoneNumber);
            //    return account;
            //}
            //throw new AppException("Email or Phone number is required");
            return null;
        }

        public async Task<AccountEntity> GetByIdAsync(long id)
        {
            //var account = await UnitOfWork.AccountRepository.GetById(id);
            //return account;
            return null;
        }

        public async Task<string> VerifyAccountAsync(string emailOrPhoneNumber, string code)
        {
            var account = await UnitOfWork.AccountRepository.GetAsync(item => item.EmailOrPhoneNumber.ToLower() == emailOrPhoneNumber.ToLower());
            
            if (account is null)
            {
                throw new AppException("Email or Phone is required");
            }
            else
            {
                //check and update confirmation email
                var confirmationEmail = await UnitOfWork.ConfirmationEmailRepository.GetAsync(item => item.AccountId == account.Id);
                if (confirmationEmail is null)
                {
                    throw new AppException("Cofirmation email is null");
                }
                if(confirmationEmail.IsUsed)
                {
                    throw new AppException("This link already used");
                }
                confirmationEmail.UpdatedAt= DateTime.UtcNow;
                confirmationEmail.IsUsed = true;
                UnitOfWork.ConfirmationEmailRepository.Update(confirmationEmail);
                
                //update account
                account.IsVerified = true;
                UnitOfWork.AccountRepository.Update(account);
                await UnitOfWork.CommitAsync();
                var user = Mapper.Map<UserEntity>(account);
                user.Email = account.EmailOrPhoneNumber.Contains('@')?account.EmailOrPhoneNumber : "";
                user.PhoneNumber = account.EmailOrPhoneNumber.StartsWith("99") ? account.EmailOrPhoneNumber : "";
                var token = JwtUtils.GenerateJwtToken(user);
                return token;
            }

        }

        public async Task<string> UpdatePassword(PasswordUpdateModel passwordUpdateModel, string id)
        {
            var account = await UnitOfWork.AccountRepository.GetAsync(item => item.Id.ToString() == id);
            
            if (account is null)
            {
                throw new AppException("Account not found");
            }
            else
            {
                if (PasswordHelper.Verify(passwordUpdateModel.Password, account.PasswordHash , account.PasswordSalt))
                {

                    var newPasswordHash = PasswordHelper.PasswordHash(passwordUpdateModel.NewPassword);
                    account.PasswordHash = newPasswordHash.hash;
                    account.PasswordSalt = newPasswordHash.salt;

                    UnitOfWork.AccountRepository.Update(account);
                    await UnitOfWork.CommitAsync();

                    var user = Mapper.Map<UserEntity>(account);
                    user.Email = account.EmailOrPhoneNumber;
                    var token = JwtUtils.GenerateJwtToken(user);

                    return token;
                }

                throw new AppException("Wrong password");
            }
        }

        public async Task<bool> UpdateAsync(AccountUpdateModel account)
        {
            var accountEntity = await UnitOfWork.AccountRepository.GetAsync(item => item.Id == account.Id);

            if (accountEntity is null)
            {
                throw new AppException("Account not found");
            }

            Mapper.Map(account, accountEntity);
            UnitOfWork.AccountRepository.Update(accountEntity);
            await UnitOfWork.CommitAsync();

            return true;
        }
    }
}
