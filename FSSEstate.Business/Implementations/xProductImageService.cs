using AutoMapper;
using FSSEstate.Business.Interfaces;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Core.Models.ProjectPhotosModels;
using FSSEstate.Core.Models.xProductImageModels;
using FSSEstate.Core.Utility.Listing;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces;

namespace FSSEstate.Business.Implementations;

public class xProductImageService : BaseService, IxProductImageService
{
    public xProductImageService(IUnitOfWork unitOfWork, IService service, IJwtUtils jwtUtils, IMapper mapper, IFileService fileService)
            : base(unitOfWork, service, jwtUtils, mapper, fileService)
    {
    }

    public async Task<bool> CreateAsync(xProductImageCreateModel photo)
    {
        var photoEntity = Mapper.Map<xProductImage>(photo);
        await UnitOfWork.XProductImageRepository.AddAsync(photoEntity);
        await UnitOfWork.CommitAsync();

        return true; 
    }
 

    public async Task<bool> DeleteAsync(long id)
    {
        var photo = await UnitOfWork.XProductImageRepository.GetAsync(item => item.Id == id);
        if (photo is null) throw new Exception("Photo not found!");

        UnitOfWork.XProductImageRepository.Remove(photo);
        await UnitOfWork.CommitAsync();

        return true;
    }
 
    public Task<PagedList<ProjectPhotoModel>> GetAllAsync(xProductImageFilterModel filterParams)
    {
        throw new NotImplementedException();
    }

    public Task<xProductImageModel> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> GetImageAsync(string path)
    {
        throw new NotImplementedException();
    }

    public Task<List<byte[]>> GetImageFiles(long projectId)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> GetMainImage(long projectId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(long id, xProductImageUpdateModel photo)
    {
        throw new NotImplementedException();
    }
}
