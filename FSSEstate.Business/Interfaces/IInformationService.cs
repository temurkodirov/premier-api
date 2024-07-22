using FSSEstate.Core.Models.InformationModels;
using FSSEstate.Core.Models.ProjectModels;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Business.Interfaces
{
    public interface IInformationService
    {
        public Task<bool> CreateAsync(InformationCreateModel information);
        public Task<PagedList<InformationModel>> GetAllAsync(InformationFilterParams filterParams);
        public Task<bool> UpdateAsync(long id, InformationUpdateModel project);
        public Task<bool> DeleteAsync(long id);
        public Task<InformationModel> GetByIdAsync(long id);
    }
}
