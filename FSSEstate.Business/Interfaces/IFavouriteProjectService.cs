using FSSEstate.Core.Models.AffairModels;
using FSSEstate.Core.Models.FavouriteProjectModels;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Business.Interfaces
{
    public interface IFavouriteProjectService
    {
        public Task<bool> CreateAsync(FavouriteProjectCreateModel project);
        public Task<bool> DeleteAsync(long id);
        public Task<PagedList<FavouriteProjectModel>> GetAllAsync(FavouriteProjectFilterParams filterParams, long accountId);
    }
}
