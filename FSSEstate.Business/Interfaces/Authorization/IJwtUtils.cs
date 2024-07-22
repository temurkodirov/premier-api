using FSSEstate.Core.Models.UserModels;
using FSSEstate.Repository.Entities;

namespace FSSEstate.Business.Interfaces.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(UserEntity user);
        public string? ValidateJwtToken(string? token);
        Task<UserClaimModel> GetUserClaimsAsync(string? token);
    }
}
