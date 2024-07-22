using AutoMapper;
using AutoMapper.QueryableExtensions;
using FSSEstate.Business.Interfaces;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Core.Models.Agents;
using FSSEstate.Core.Models.InformationModels;
using FSSEstate.Core.Models.InformationPhotosModels;
using FSSEstate.Core.Models.ProjectModels;
using FSSEstate.Core.Models.ProjectPhotosModels;
using FSSEstate.Core.Utility.Listing;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FSSEstate.Business.Implementations
{
    public class InformationService : BaseService, IInformationService
    {
        public InformationService(IUnitOfWork unitOfWork, IService service, IJwtUtils jwtUtils, IMapper mapper, IFileService fileService) : base(unitOfWork, service, jwtUtils, mapper, fileService)
        {
        }

        public async Task<bool> CreateAsync(InformationCreateModel information)
        {
            var informationEntity = Mapper.Map<InformationEntity>(information);
            await UnitOfWork.InformationRepository.AddAsync(informationEntity);
            await UnitOfWork.CommitAsync();

            if (informationEntity.Id != 0)
            {
                try
                {
                    int countImages = 0;
                    foreach (var item in information.Images)
                    {
                        countImages++;
                        var imgPath = await FileService.UploadImageAsync(item, "Information");

                        var projectImageEntity = new InformationPhotosEntity
                        {
                            InformationId = informationEntity.Id,
                            ImagePath = imgPath,
                            IsMain = countImages == 1 ? true : false,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };

                        await UnitOfWork.InformationPhotosRepository.AddAsync(projectImageEntity);
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
            try
            {
                var info = await UnitOfWork.InformationRepository.GetAsync(item => item.Id == id);
                if (info is null) throw new Exception("Information not found!");

                var photos = await UnitOfWork.InformationPhotosRepository.GetAllByQueryAsync(item => item.InformationId == info.Id);
                if (!photos.IsNullOrEmpty())
                {
                    foreach (var photo in photos)
                    {
                        await FileService.DeleteImageAsync(photo.ImagePath);
                    }
                    UnitOfWork.InformationPhotosRepository.RemoveRange(photos);
                }

                UnitOfWork.InformationRepository.Remove(info);
                await UnitOfWork.CommitAsync();

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PagedList<InformationModel>> GetAllAsync(InformationFilterParams filterParams)
        {
            var entityItems = await UnitOfWork.InformationRepository.GetAllByQueryAsync(item =>
                (filterParams.SearchText == string.Empty || item.Title.Contains(filterParams.SearchText)),
                 null, x => x.CreatedAt,
                filterParams.Order == "desc");
            var items = entityItems.ProjectTo<InformationModel>(Mapper.ConfigurationProvider);
            PagedList<InformationModel> pagedList = PagedList<InformationModel>.ToPagedListFromQuery(
                filterParams.PageNumber,
                filterParams.PageSize,
                filterParams.Order,
                items);

            foreach (var information in pagedList)
            {
                var imageItem = await UnitOfWork.InformationPhotosRepository.GetAsync(item => item.InformationId == information.Id && item.IsMain);
                if (imageItem is not null)
                {
                    var image = Mapper.Map<InformationPhotoModel>(imageItem);

                    if (information.Images == null)
                        information.Images = new List<InformationPhotoModel>();

                    information.Images.Add(image);
                }
            }

            return pagedList;
        }

        public async Task<InformationModel> GetByIdAsync(long id)
        {
            var informationEntity = await UnitOfWork.InformationRepository.GetAsync(item => item.Id == id);
            if (informationEntity is null) throw new Exception("Information not found!");

            var informationItem = Mapper.Map<InformationModel>(informationEntity);

            var imageItems = await UnitOfWork.InformationPhotosRepository.GetAllByQueryAsync(item => item.InformationId == informationItem.Id);
            if (!imageItems.IsNullOrEmpty())
            {
                var images = imageItems.ProjectTo<InformationPhotoModel>(Mapper.ConfigurationProvider);

                if (informationItem.Images == null)
                    informationItem.Images = new List<InformationPhotoModel>();

                informationItem.Images.AddRange(images);
            }
            return informationItem;
        }

        public async Task<bool> UpdateAsync(long id, InformationUpdateModel project)
        {
            var informationEntity = await UnitOfWork.InformationRepository.GetAsync(item => item.Id == id);
            if (informationEntity is null) throw new Exception("Information not found!");

            Mapper.Map(project, informationEntity);
            UnitOfWork.InformationRepository.Update(informationEntity);
            await UnitOfWork.CommitAsync();
            return true;
        }
    }
}
