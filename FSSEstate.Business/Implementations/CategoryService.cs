using AutoMapper;
using AutoMapper.QueryableExtensions;
using FSSEstate.Business.Interfaces;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Core.Models.ProjectCategoryModels;
using FSSEstate.Core.Utility.Listing;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FSSEstate.Business.Implementations
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IService service, IJwtUtils jwtUtils, IMapper mapper, IFileService fileService) 
            : base(unitOfWork, service, jwtUtils, mapper, fileService)
        {
        }

        public async Task<bool> CreateAsync(CategoryCreateModel projectCategory)
        {
            var projectCategoryEntity = Mapper.Map<CategoryEntity>(projectCategory);
            var imagePath = await FileService.UploadImageAsync(projectCategory.Image, "Category");
            projectCategoryEntity.Image = imagePath;
            
            await UnitOfWork.CategoryRepository.AddAsync(projectCategoryEntity);
            await UnitOfWork.CommitAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var category = await UnitOfWork.CategoryRepository.GetAsync(item => item.Id == id);
            if (category is null) throw new Exception("Project category not found!");
            if(!string.IsNullOrEmpty(category.Image))
                await FileService.DeleteImageAsync(category.Image);
            UnitOfWork.CategoryRepository.Remove(category);
            await UnitOfWork.CommitAsync();

            return true;
        }

        public async Task<PagedList<CategoryModel>> GetAllAsync(CategoryFilterParams filterParams)
        {
            var entityItems = await UnitOfWork.CategoryRepository.GetAllByQueryAsync(item =>
              (filterParams.SearchText == string.Empty || item.Name.Contains(filterParams.SearchText)),
               null, x => x.CreatedAt,
              filterParams.Order == "desc");
            var items = entityItems.ProjectTo<CategoryModel>(Mapper.ConfigurationProvider);

            PagedList<CategoryModel> pagedList = PagedList<CategoryModel>.ToPagedListFromQuery(
                filterParams.PageNumber,
                filterParams.PageSize,
                filterParams.Order,
                items);

            return pagedList;
        }
        public async Task<PagedList<CategoryModel>> GetAllMainAsync(CategoryFilterParams filterParams)
        {
            var entityItems = await UnitOfWork.CategoryRepository.GetAllByQueryAsync(item =>
              (filterParams.SearchText == string.Empty || item.Name.ToLower().Contains(filterParams.SearchText.ToLower())&&
              item.ParentId != null),
              null, x => x.CreatedAt,
              filterParams.Order == "desc");
            var items = entityItems.ProjectTo<CategoryModel>(Mapper.ConfigurationProvider);

            PagedList<CategoryModel> pagedList = PagedList<CategoryModel>.ToPagedListFromQuery(
                filterParams.PageNumber,
                filterParams.PageSize,
                filterParams.Order,
                items);

            return pagedList;
        }

        public async Task<CategoryModel> GetByIdAsync(long id)
        {
            var projectEntity = await UnitOfWork.CategoryRepository.GetAsync(item => item.Id == id);
            if (projectEntity is null) throw new Exception("Project category not found!");

            var project = Mapper.Map<CategoryModel>(projectEntity);
            return project;
        }

        public async Task<bool> UpdateAsync(long id, CategoryUpdateModel project)
        {
            var projectCategoryEntity = await UnitOfWork.CategoryRepository.GetAsync(item => item.Id == id);
            if (projectCategoryEntity is null) throw new Exception("Project category not found!");
            Mapper.Map(project, projectCategoryEntity);
            if (project.Image is not null)
            {
                if (!string.IsNullOrEmpty(projectCategoryEntity.Image))
                    await FileService.DeleteImageAsync(projectCategoryEntity.Image);
                
                var imagePath = await FileService.UploadImageAsync(project.Image, "Category");
                projectCategoryEntity.Image = imagePath;
            }
                        
            UnitOfWork.CategoryRepository.Update(projectCategoryEntity);
            await UnitOfWork.CommitAsync();
            return true;
        }
    }
}
