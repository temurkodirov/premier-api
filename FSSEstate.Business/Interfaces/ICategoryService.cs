using FSSEstate.Core.Models.ProjectCategoryModels;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Business.Interfaces
{
    public interface ICategoryService
    {
        public Task<bool> CreateAsync(CategoryCreateModel projectCategory);
        public Task<PagedList<CategoryModel>> GetAllAsync(CategoryFilterParams filterParams);
        public Task<PagedList<CategoryModel>> GetAllMainAsync(CategoryFilterParams filterParams);
        public Task<bool> UpdateAsync(long id, CategoryUpdateModel category);
        public Task<bool> DeleteAsync(long id);
        public Task<CategoryModel> GetByIdAsync(long id);
    }
}
