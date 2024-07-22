using FSSEstate.Core.Models.Agents;
using FSSEstate.Core.Models.ProjectPhotosModels;
using FSSEstate.Core.Utility.Listing;
using Microsoft.AspNetCore.Http;

namespace FSSEstate.Business.Interfaces
{
    public interface IProjectPhotoService
    {
        public Task<bool> CreateAsync(ProjectPhotoCreateModel photo);
        public Task<PagedList<ProjectPhotoModel>> GetAllAsync(ProjectPhotoFilterParams filterParams);
        public Task<bool> UpdateAsync(long id, ProjectPhotoUpdateModel photo);
        public Task<bool> DeleteAsync(long id);
        public Task<ProjectPhotoModel> GetByIdAsync(long id);
        public Task<List<byte[]>> GetImageFiles(long projectId);
        public Task<byte[]> GetMainImage(long projectId);
        public Task<byte[]> GetImageAsync(string path);
    }
}
