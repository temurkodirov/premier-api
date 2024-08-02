using AutoMapper;
using AutoMapper.QueryableExtensions;
using FSSEstate.Business.Interfaces;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Core.Models.xProductCharacteristicsModels;
using FSSEstate.Core.Utility.Listing;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces;

namespace FSSEstate.Business.Implementations;

public class xProductCharacteristicsService : BaseService, IxProductCharacteristicsService
{
    public xProductCharacteristicsService(IUnitOfWork unitOfWork,
                            IService service,
                            IJwtUtils jwtUtils,
    IMapper mapper,
                            IFileService fileService) : base(unitOfWork,
                                                                service,
                                                                jwtUtils,
                                                                mapper,
                                                                fileService)
    {


    }
    public async Task<bool> CreateAsync(xProductCharacteristicsCreateModel characterx)
    {
        var characterEntity = Mapper.Map<xProductCharacteristics>(characterx);
        await UnitOfWork.XProductCharacteristicsRepository.AddAsync(characterEntity);
        await UnitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var characterx = await UnitOfWork.XProductCharacteristicsRepository.GetAsync(item => item.Id == id);
        if (characterx is null) throw new Exception("Photo not found!");
        UnitOfWork.XProductCharacteristicsRepository.Remove(characterx);
        await UnitOfWork.CommitAsync();

        return true;
    }

    public async Task<PagedList<xProductCharacteristicsModel>> GetAllAsync(xProductCharacteristicsFilterModel filterParams)
    {
        var entityItems = await UnitOfWork.XProductCharacteristicsRepository.GetAllByQueryAsync(item =>
        item.ProductId == filterParams.xProductId, null, x => x.CreatedAt, filterParams.Order == "desc");

        var items = entityItems.ProjectTo<xProductCharacteristicsModel>(Mapper.ConfigurationProvider);
        PagedList<xProductCharacteristicsModel> pagedList = PagedList<xProductCharacteristicsModel>.ToPagedListFromQuery(
            filterParams.PageNumber,
            filterParams.PageSize = 60,
            filterParams.Order,
            items
            );

        return pagedList;
    }


    public Task<xProductCharacteristicsModel> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(long id, xProductCharacteristicsUpdateModel characterx)
    {
        var characterEntity = await UnitOfWork.XProductCharacteristicsRepository.GetAsync(item => item.Id == id);
        if (characterEntity is null) throw new Exception("Characteristics not found");

        Mapper.Map(characterx, characterEntity);
        UnitOfWork.XProductCharacteristicsRepository.Update(characterEntity);

        await UnitOfWork.CommitAsync();
        return true;
    }

}
