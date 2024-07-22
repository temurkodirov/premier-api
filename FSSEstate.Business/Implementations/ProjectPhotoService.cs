using AutoMapper;
using AutoMapper.QueryableExtensions;
using FSSEstate.Business.Interfaces;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Core.Models.ProjectPhotosModels;
using FSSEstate.Core.Utility.Listing;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FSSEstate.Business.Implementations;

public class ProjectPhotoService : BaseService, IProjectPhotoService
{
    public ProjectPhotoService(IUnitOfWork unitOfWork, IService service, IJwtUtils jwtUtils, IMapper mapper, IFileService fileService) 
        : base(unitOfWork, service, jwtUtils, mapper, fileService)
    {
    }

    public async Task<bool> CreateAsync(ProjectPhotoCreateModel photo)
    {
        var photoEntity = Mapper.Map<ProjectPhotosEntity>(photo);
        await UnitOfWork.ProjectPhotosRepository.AddAsync(photoEntity);
        await UnitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var photo = await UnitOfWork.ProjectPhotosRepository.GetAsync(item => item.Id == id);
        if (photo is null) throw new Exception("Photo not found!");

        UnitOfWork.ProjectPhotosRepository.Remove(photo);
        await UnitOfWork.CommitAsync();

        return true;
    }

    public async Task<PagedList<ProjectPhotoModel>> GetAllAsync(ProjectPhotoFilterParams filterParams)
    {
        var entityItems = await UnitOfWork.ProjectPhotosRepository.GetAllByQueryAsync(item =>
            item.ProjectId == filterParams.ProjectId,
           null, x => x.CreatedAt,
          filterParams.Order == "desc");
        var items = entityItems.ProjectTo<ProjectPhotoModel>(Mapper.ConfigurationProvider);
        PagedList<ProjectPhotoModel> pagedList = PagedList<ProjectPhotoModel>.ToPagedListFromQuery(
            filterParams.PageNumber,
            filterParams.PageSize,
            filterParams.Order,
            items);

        return pagedList;
    }

    public async Task<ProjectPhotoModel> GetByIdAsync(long id)
    {
        var photoEntity = await UnitOfWork.ProjectPhotosRepository.GetAsync(item => item.Id == id);
        if (photoEntity is null) throw new Exception("Photo not found!");

        var photo = Mapper.Map<ProjectPhotoModel>(photoEntity);
        return photo;
    }

    public async Task<List<byte[]>> GetImageFiles(long projectId)
    {
        var result = new List<byte[]>();
        var photos = await UnitOfWork.ProjectPhotosRepository.GetAllAsync(item => item.ProjectId == projectId, null);
        foreach(var item in photos)
        {
            var photo = await FileService.GetImageAsync(item.ImagePath);
            result.Add(photo);
        }

        return result;
    }

    public async Task<byte[]> GetImageAsync(string path)
    {
        var result = await FileService.GetImageAsync(path);
        return result;
    }
    public async Task<byte[]> GetMainImage(long projectId)
    {
        var image = await UnitOfWork.ProjectPhotosRepository.GetAsync(item => item.ProjectId == projectId && item.IsMain);
        var result = await FileService.GetImageAsync(image.ImagePath);
        return result;
    }
    public async Task<bool> UpdateAsync(long id, ProjectPhotoUpdateModel photo)
    {
        var photoEntity = await UnitOfWork.ProjectPhotosRepository.GetAsync(item => item.Id == id);
        if (photoEntity is null) throw new Exception("Photo not found!");

        Mapper.Map(photo, photoEntity);
        UnitOfWork.ProjectPhotosRepository.Update(photoEntity);
        await UnitOfWork.CommitAsync();
        return true;
    }
}
