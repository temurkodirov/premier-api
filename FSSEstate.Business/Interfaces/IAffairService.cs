using FSSEstate.Core.Models.AffairModels;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Business.Interfaces
{
    public interface IAffairService
    {
        public Task<bool> CreateAsync(AffairCreateModel affair);
        public Task<PagedList<AffairModel>> GetAllAsync(AffairFilterParams filterParams);
        public Task<bool> UpdateAsync(long id, AffairUpdateModel affair);
        public Task<bool> DeleteAsync(long id);
        public Task<AffairModel> GetByIdAsync(long id);
    }
}
