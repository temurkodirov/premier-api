using FSSEstate.Core.Models.ProjectPhotosModels;
using FSSEstate.Core.Models.xProductImageModels;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Business.Interfaces;

public interface IxProductImageService
{
    public Task<bool> CreateAsync(xProductImageCreateModel photo);
    public Task<PagedList<ProjectPhotoModel>> GetAllAsync(xProductImageFilterModel filterParams);
    public Task<bool> UpdateAsync(long id, xProductImageUpdateModel photo);
    public Task<bool> DeleteAsync(long id);
    public Task<xProductImageModel> GetByIdAsync(long id);
    public Task<List<byte[]>> GetImageFiles(long projectId);
    public Task<byte[]> GetMainImage(long projectId);
    public Task<byte[]> GetImageAsync(string path);
}
