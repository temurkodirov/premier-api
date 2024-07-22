using FSSEstate.Core.Models.InformationPhotosModels;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Business.Interfaces
{
    public interface IInformationPhotoService
    {
        public Task<bool> CreateAsync(InformationPhotoCreateModel photo);
        public Task<PagedList<InformationPhotoModel>> GetAllAsync(InformationPhotoFilterParams filterParams);
        public Task<bool> UpdateAsync(long id, InformationPhotoUpdateModel photo);
        public Task<bool> DeleteAsync(long id);
        public Task<InformationPhotoModel> GetByIdAsync(long id);
        public Task<List<byte[]>> GetImageFiles(long informationId);
        public Task<byte[]> GetMainImage(long informationId);
        public Task<byte[]> GetImageAsync(string path);
    }
}
