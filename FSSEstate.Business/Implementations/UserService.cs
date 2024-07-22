using AutoMapper;
using FSSEstate.Business.Implementations.Helpers;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Business.Interfaces;
using FSSEstate.Repository.Interfaces;
using FSSEstate.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using FSSEstate.Core.Models.UserModels;
using FSSEstate.Repository.Entities;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Business.Implementations
{
    public class UserService : BaseService, IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IService services, IJwtUtils jwtUtils, IHttpContextAccessor httpContext, IMapper mapper, IFileService fileService) 
            : base(unitOfWork, services, jwtUtils, mapper, fileService)
        {
            _httpContextAccessor = httpContext;
            _mapper = mapper;
        }

        public async Task<string> LoginAsync(UserLogin userLoginRequest)
        {
            var user = await UnitOfWork.AccountRepository.GetAsync(item => item.EmailOrPhoneNumber.ToLower() == userLoginRequest.EmailOrPhoneNumber.ToLower());
            if (user is null)
            {
                throw new AppException("Account not Found");
            }
            var passwordVerify = PasswordHelper.Verify(userLoginRequest.Password, user.PasswordHash, user.PasswordSalt);
            if (!user.IsVerified)
            {
                throw new AppException("Inactive user error");
            }
            if (passwordVerify)
            {
                var account = Mapper.Map<UserEntity>(user);
                account.Email = user.EmailOrPhoneNumber;
                var token = JwtUtils.GenerateJwtToken(account);
                return token;
            }
            else
            {
                throw new AppException("Username or password is incorrect");
            }
        }
        public async Task<bool> CreateAsync(UserCreateModel usermodel)
        {
            var account = UnitOfWork.AccountRepository.GetAsync(item=>item.EmailOrPhoneNumber.ToLower() == usermodel.Email.ToLower() || item.EmailOrPhoneNumber == usermodel.PhoneNumber).Result;
            if (account.IsVerified)
            {
                var userResult = await UnitOfWork.UserRepository.GetAsync(item => item.Email.ToLower() == usermodel.Email.ToLower() || item.PhoneNumber == usermodel.PhoneNumber);
                if (userResult is not null)
                {
                    account.Fullname = usermodel.Fullname;
                    account.UpdatedAt = DateTime.UtcNow;
                    UnitOfWork.AccountRepository.Update(account);
                    await UnitOfWork.CommitAsync();

                    Mapper.Map(usermodel, userResult);
                    UnitOfWork.UserRepository.Update(userResult);
                    await UnitOfWork.CommitAsync();
                    return true;
                }
                // map model to new user object
                var user = _mapper.Map<UserEntity>(usermodel);
                user.AccountId = account.Id;

                account.Fullname = usermodel.Fullname;
                account.UpdatedAt = DateTime.UtcNow;
                UnitOfWork.AccountRepository.Update(account);
                await UnitOfWork.CommitAsync();

                // save user
                await UnitOfWork.UserRepository.AddAsync(user);
                await UnitOfWork.CommitAsync();
                return true;
            }
            else
                throw new AppException("Account wasn't verified");            
        }

        public async Task<UserModel> GetByIdAsync(long id)
        {
            var account = await UnitOfWork.AccountRepository.GetAsync(item => item.Id == id);
            if (account is not null)
            {
                var user = new UserEntity();
                var result = new UserModel();

                if(account.EmailOrPhoneNumber.StartsWith("99"))                
                    user = await UnitOfWork.UserRepository.GetAsync(item => item.PhoneNumber == account.EmailOrPhoneNumber);                    
                
                if (account.EmailOrPhoneNumber.Contains("@"))
                    user = await UnitOfWork.UserRepository.GetAsync(item => item.Email == account.EmailOrPhoneNumber);

                result = Mapper.Map<UserModel>(user);
                return result;
            }
            else
                return null;
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var userList = await UnitOfWork.UserRepository.GetAllAsync();
            var resultList = Mapper.Map<IEnumerable<UserModel>>(userList);
            return resultList;
        }

        public async Task<UserClaimModel> GetUserClaimsAsync(string token)
        {
            return await JwtUtils.GetUserClaimsAsync(token);
        }

        public async Task<PagedList<UserModel>> GetAllAsync(UserFilterParams filterParams)
        {
            var entityItems = await UnitOfWork.UserRepository.GetAllAsync(item =>
                (filterParams.SearchText == string.Empty || item.Fullname.Contains(filterParams.SearchText)),
                 null, x => x.CreatedAt,
                filterParams.Order == "desc");
            var items = Mapper.Map<IEnumerable<UserModel>>(entityItems);
            PagedList<UserModel> pagedList = PagedList<UserModel>.ToPagedList(
                filterParams.PageNumber,
                filterParams.PageSize,
                filterParams.Order,
                items);

            return pagedList;
        }
    }
}
