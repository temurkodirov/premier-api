using AutoMapper;
using AutoMapper.QueryableExtensions;
using FSSEstate.Business.Interfaces;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Core.Models.InformationPhotosModels;
using FSSEstate.Core.Models.ProjectPhotosModels;
using FSSEstate.Core.Utility.Listing;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces;

namespace FSSEstate.Business.Implementations
{
    public class InformationPhotoService : BaseService, IInformationPhotoService
    {
        public InformationPhotoService(IUnitOfWork unitOfWork, IService service, IJwtUtils jwtUtils, IMapper mapper, IFileService fileService) : base(unitOfWork, service, jwtUtils, mapper, fileService)
        {
        }

        public async Task<bool> CreateAsync(InformationPhotoCreateModel photo)
        {
            var photoEntity = Mapper.Map<InformationPhotosEntity>(photo);
            await UnitOfWork.InformationPhotosRepository.AddAsync(photoEntity);
            await UnitOfWork.CommitAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var photo = await UnitOfWork.InformationPhotosRepository.GetAsync(item => item.Id == id);
            if (photo is null) throw new Exception("Information photo not found!");

            UnitOfWork.InformationPhotosRepository.Remove(photo);
            await UnitOfWork.CommitAsync();

            return true;
        }

        public async Task<PagedList<InformationPhotoModel>> GetAllAsync(InformationPhotoFilterParams filterParams)
        {
            var entityItems = await UnitOfWork.InformationPhotosRepository.GetAllByQueryAsync(item =>
                item.InformationId == filterParams.InformationId,
               null, x => x.CreatedAt,
              filterParams.Order == "desc");
            var items = entityItems.ProjectTo<InformationPhotoModel>(Mapper.ConfigurationProvider);
            PagedList<InformationPhotoModel> pagedList = PagedList<InformationPhotoModel>.ToPagedListFromQuery(
                filterParams.PageNumber,
                filterParams.PageSize,
                filterParams.Order,
                items);

            return pagedList;
        }

        public async Task<InformationPhotoModel> GetByIdAsync(long id)
        {
            var photoEntity = await UnitOfWork.InformationRepository.GetAsync(item => item.Id == id);
            if (photoEntity is null) throw new Exception("Information photo not found!");

            var photo = Mapper.Map<InformationPhotoModel>(photoEntity);
            return photo;
        }

        public async Task<byte[]> GetImageAsync(string path)
        {
            var result = await FileService.GetImageAsync(path);
            return result;
        }

        public async Task<List<byte[]>> GetImageFiles(long informationId)
        {
            var result = new List<byte[]>();
            var photos = await UnitOfWork.InformationPhotosRepository.GetAllAsync(item => item.Id == informationId, null);
            foreach (var item in photos)
            {
                var photo = await FileService.GetImageAsync(item.ImagePath);
                result.Add(photo);
            }

            return result;
        }

        public async Task<byte[]> GetMainImage(long informationId)
        {
            var image = await UnitOfWork.InformationPhotosRepository.GetAsync(item => item.InformationId == informationId && item.IsMain);
            var result = await FileService.GetImageAsync(image.ImagePath);
            return result;
        }

        public async Task<bool> UpdateAsync(long id, InformationPhotoUpdateModel photo)
        {
            var photoEntity = await UnitOfWork.InformationPhotosRepository.GetAsync(item => item.Id == id);
            if (photoEntity is null) throw new Exception("Informmaton photo not found!");

            Mapper.Map(photo, photoEntity);
            UnitOfWork.InformationPhotosRepository.Update(photoEntity);
            await UnitOfWork.CommitAsync();
            return true;
        }
    }
}
