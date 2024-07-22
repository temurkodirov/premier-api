using AutoMapper;
using AutoMapper.QueryableExtensions;
using FSSEstate.Business.Interfaces;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Core.Models.AffairModels;
using FSSEstate.Core.Models.FavouriteProjectModels;
using FSSEstate.Core.Models.ProjectPhotosModels;
using FSSEstate.Core.Utility.Listing;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FSSEstate.Business.Implementations
{
    public class FavouriteProjectService : BaseService, IFavouriteProjectService
    {
        public FavouriteProjectService(IUnitOfWork unitOfWork, IService service, IJwtUtils jwtUtils, IMapper mapper, IFileService fileService) 
            : base(unitOfWork, service, jwtUtils, mapper, fileService)
        {
        }

        public async Task<bool> CreateAsync(FavouriteProjectCreateModel project)
        {
            var favouriteProjectEntity = Mapper.Map<FavouriteProjectEntity>(project);
            var items = await UnitOfWork.FavouriteProjectRepository.GetAllByQueryAsync(item => (item.AccountId == project.AccountId) &&
                (item.ProjectId == project.ProjectId));
            if(items.Any())            
                throw new Exception("Already exist");
            
            await UnitOfWork.FavouriteProjectRepository.AddAsync(favouriteProjectEntity);
            await UnitOfWork.CommitAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var favouriteProjectEntity = await UnitOfWork.FavouriteProjectRepository.GetAsync(item => item.Id == id);
            if (favouriteProjectEntity is null) throw new Exception("Favourite project not found!");

            UnitOfWork.FavouriteProjectRepository.Remove(favouriteProjectEntity);
            await UnitOfWork.CommitAsync();

            return true;
        }

        public async Task<PagedList<FavouriteProjectModel>> GetAllAsync(FavouriteProjectFilterParams filterParams, long accountId)
        {
            var entityItems = await UnitOfWork.FavouriteProjectRepository.GetAllByQueryAsync(item=>(item.AccountId == accountId)&&
            (filterParams.CategoryId == null || item.Project.CategoryId == filterParams.CategoryId) &&
            (filterParams.CategoryId == null || item.Project.Category.ParentId == filterParams.CategoryId),
                query => query.Include(y => y.Project).Include(z=>z.Project), x => x.CreatedAt,
               filterParams.Order == "desc");
            var items = entityItems.ProjectTo<FavouriteProjectModel>(Mapper.ConfigurationProvider);
            PagedList<FavouriteProjectModel> pagedList = PagedList<FavouriteProjectModel>.ToPagedListFromQuery(
                filterParams.PageNumber,
                filterParams.PageSize,
                filterParams.Order,
                items);
            foreach (var projectItem in pagedList)
            {
                var imageItem = await UnitOfWork.ProjectPhotosRepository.GetAsync(item => item.ProjectId == projectItem.Project.Id && item.IsMain);
                if (imageItem is not null)
                {
                    var image = Mapper.Map<ProjectPhotoModel>(imageItem);

                    if (projectItem.Project.Images == null)
                        projectItem.Project.Images = new List<ProjectPhotoModel>();

                    projectItem.Project.Images.Add(image);
                }
            }
            return pagedList;
        }
    }
}
