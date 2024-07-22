using FSSEstate.Core.Models.ProjectModels;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Business.Interfaces
{
    public interface IProjectService
    {
        public Task<bool> CreateAsync(ProjectCreateModel project);
        public Task<PagedList<ProjectModel>> GetAllAsync(ProjectFilterParams filterParams);
        public Task<PagedList<ProjectModel>> GetAllByUserIdAsync(ProjectFilterParams filterParams, long userId);
        public Task<bool> UpdateAsync(long id, ProjectUpdateModel project);
        public Task<bool> DeleteAsync(long id);
        public Task<ProjectModel> GetByIdAsync(long id);
    }
}
