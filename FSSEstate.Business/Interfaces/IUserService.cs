using FSSEstate.Core.Models.AccountModels;
using FSSEstate.Core.Models.ProjectModels;
using FSSEstate.Core.Models.UserModels;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Business.Interfaces
{
    public interface IUserService
    {
        public Task<string> LoginAsync(UserLogin userLoginRequest);
        public Task<bool> CreateAsync(UserCreateModel userCreateRequest);
        public Task<UserModel> GetByIdAsync(long id);
        public Task<IEnumerable<UserModel>> GetAllAsync();
        public Task<PagedList<UserModel>> GetAllAsync(UserFilterParams filterParams);
        public Task<UserClaimModel> GetUserClaimsAsync(string token);

    }
}
