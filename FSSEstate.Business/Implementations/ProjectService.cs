using AutoMapper;
using AutoMapper.QueryableExtensions;
using FSSEstate.Business.Interfaces;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Core.Models.ProjectModels;
using FSSEstate.Core.Models.ProjectPhotosModels;
using FSSEstate.Core.Utility.Listing;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FSSEstate.Business.Implementations
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(IUnitOfWork unitOfWork, IService service, IJwtUtils jwtUtils, IMapper mapper, IFileService fileService) : base(unitOfWork, service, jwtUtils, mapper, fileService)
        {
        }

        public async Task<bool> CreateAsync(ProjectCreateModel project)
        {
            var projectEntity = Mapper.Map<ProjectEntity>(project);
            await UnitOfWork.ProjectRepository.AddAsync(projectEntity);
            await UnitOfWork.CommitAsync();

            if (projectEntity.Id != 0) 
            {
                try
                {
                    int countImages = 0;
                    foreach (var item in project.Images)
                    {
                        countImages++;
                        var imgPath = await FileService.UploadImageAsync(item, "Project");

                        var projectImageEntity = new ProjectPhotosEntity
                        {
                            ProjectId = projectEntity.Id,
                            ImagePath = imgPath,
                            IsMain = countImages == 1 ? true : false,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };

                        await UnitOfWork.ProjectPhotosRepository.AddAsync(projectImageEntity);
                        await UnitOfWork.CommitAsync();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return false;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var project = await UnitOfWork.ProjectRepository.GetAsync(item => item.Id == id);
            if (project is null) throw new Exception("Project not found!");

            var photos = await UnitOfWork.ProjectPhotosRepository.GetAllByQueryAsync(item => item.ProjectId == project.Id);
            if (!photos.IsNullOrEmpty())
            {
                foreach (var photo in photos)
                {
                    await FileService.DeleteImageAsync(photo.ImagePath);
                }
                UnitOfWork.ProjectPhotosRepository.RemoveRange(photos);
            }
            
            UnitOfWork.ProjectRepository.Remove(project);
            await UnitOfWork.CommitAsync();

            return true;
        }

        public async Task<PagedList<ProjectModel>> GetAllAsync(ProjectFilterParams filterParams)
        {
            var entityItems = await UnitOfWork.ProjectRepository.GetAllByQueryAsync(item =>
               (filterParams.SearchText == string.Empty || item.PropertyTitle.ToLower().Contains(filterParams.SearchText.ToLower())) &&
               (filterParams.CategoryId == null || item.CategoryId == filterParams.CategoryId) &&
               (filterParams.Status == null || item.Status == filterParams.Status) &&
               (filterParams.MaxPrice == null || item.Price < filterParams.MaxPrice) &&
               (filterParams.MinPrice == null || item.Price > filterParams.MinPrice) &&
               (filterParams.Address.IsNullOrEmpty() || item.Address.ToLower().Contains(filterParams.Address)),
               null, x => x.CreatedAt,
               filterParams.Order == "desc");

            var projectItems = entityItems.ProjectTo<ProjectModel>(Mapper.ConfigurationProvider);
            
            PagedList<ProjectModel> pagedList = PagedList<ProjectModel>.ToPagedListFromQuery(
                filterParams.PageNumber,
                filterParams.PageSize,
                filterParams.Order,
                projectItems);

            foreach (var projectItem in pagedList)
            {
                var imageItem = await UnitOfWork.ProjectPhotosRepository.GetAsync(item => item.ProjectId == projectItem.Id && item.IsMain);
                if (imageItem is not null)
                {
                    var image = Mapper.Map<ProjectPhotoModel>(imageItem);

                    if (projectItem.Images == null)
                        projectItem.Images = new List<ProjectPhotoModel>();

                    projectItem.Images.Add(image);
                }
            }

            return pagedList;
        }

        public async Task<PagedList<ProjectModel>> GetAllByUserIdAsync(ProjectFilterParams filterParams, long userId)
        {
            var entityItems = await UnitOfWork.ProjectRepository.GetAllByQueryAsync(item =>
                (filterParams.SearchText == string.Empty || item.PropertyTitle.ToLower().Contains(filterParams.SearchText.ToLower())) &&
                (filterParams.CategoryId == null || item.CategoryId == filterParams.CategoryId) &&
                (userId == item.AccountId) &&
                (filterParams.Status == null || item.Status == filterParams.Status) &&
                (filterParams.MaxPrice == null || item.Price < filterParams.MaxPrice) &&
                (filterParams.MinPrice == null || item.Price > filterParams.MinPrice) &&
                (filterParams.Address.IsNullOrEmpty() || item.Address.ToLower().Contains(filterParams.Address)),
                null, x => x.CreatedAt,
                filterParams.Order == "desc");

            var projectItems = entityItems.ProjectTo<ProjectModel>(Mapper.ConfigurationProvider);

            PagedList<ProjectModel> pagedList = PagedList<ProjectModel>.ToPagedListFromQuery(
                filterParams.PageNumber,
                filterParams.PageSize,
                filterParams.Order,
                projectItems);

            foreach (var projectItem in pagedList)
            {
                var imageItem = await UnitOfWork.ProjectPhotosRepository.GetAsync(item => item.ProjectId == projectItem.Id && item.IsMain);
                if (imageItem is not null)
                {
                    var image = Mapper.Map<ProjectPhotoModel>(imageItem);

                    if (projectItem.Images == null)
                        projectItem.Images = new List<ProjectPhotoModel>();

                    projectItem.Images.Add(image);
                }
            }

            return pagedList;
        }

        public async Task<ProjectModel> GetByIdAsync(long id)
        {
            var projectEntity = await UnitOfWork.ProjectRepository.GetAsync(item => item.Id == id);
            if (projectEntity is null) throw new Exception("Project not found!");

            var projectItem = Mapper.Map<ProjectModel>(projectEntity);

            var imageItems = await UnitOfWork.ProjectPhotosRepository.GetAllByQueryAsync(item => item.ProjectId == projectItem.Id);
            if (!imageItems.IsNullOrEmpty())
            {
                var images = imageItems.ProjectTo<ProjectPhotoModel>(Mapper.ConfigurationProvider);

                if (projectItem.Images == null)
                    projectItem.Images = new List<ProjectPhotoModel>();

                projectItem.Images.AddRange(images);
            }
            return projectItem;
        }

        public async Task<bool> UpdateAsync(long id, ProjectUpdateModel project)
        {
            var projectEntity = await UnitOfWork.ProjectRepository.GetAsync(item => item.Id == id);
            if (projectEntity is null) throw new Exception("Project not found!");

            Mapper.Map(project, projectEntity);
            UnitOfWork.ProjectRepository.Update(projectEntity);
            await UnitOfWork.CommitAsync();
            return true;
        }
    }
}
