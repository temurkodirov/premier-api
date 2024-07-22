using AutoMapper;
using AutoMapper.QueryableExtensions;
using FSSEstate.Business.Interfaces;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Core.Models.AffairModels;
using FSSEstate.Core.Utility.Listing;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces;

namespace FSSEstate.Business.Implementations
{
    public class AffairService : BaseService, IAffairService
    {
        public AffairService(IUnitOfWork unitOfWork, IService service, IJwtUtils jwtUtils, IMapper mapper, IFileService fileService) : base(unitOfWork, service, jwtUtils, mapper, fileService)
        {
        }

        public async Task<bool> CreateAsync(AffairCreateModel affair)
        {
            var affairEntity = Mapper.Map<AffairEntity>(affair);
            await UnitOfWork.AffairRepository.AddAsync(affairEntity);
            await UnitOfWork.CommitAsync();

            return true;
        }       
        public async Task<bool> DeleteAsync(long id)
        {
            var affair = await UnitOfWork.AffairRepository.GetAsync(item => item.Id == id);
            if (affair is null) throw new Exception("Affair not found!");

            UnitOfWork.AffairRepository.Remove(affair);
            await UnitOfWork.CommitAsync();

            return true;
        }

        public async Task<PagedList<AffairModel>> GetAllAsync(AffairFilterParams filterParams)
        {
            var entityItems = await UnitOfWork.AffairRepository.GetAllByQueryAsync(item =>
              (filterParams.SearchText == string.Empty || item.Name.Contains(filterParams.SearchText)),
               null, x => x.CreatedAt,
              filterParams.Order == "desc");
            var items = entityItems.ProjectTo<AffairModel>(Mapper.ConfigurationProvider);
            PagedList<AffairModel> pagedList = PagedList<AffairModel>.ToPagedListFromQuery(
                filterParams.PageNumber,
                filterParams.PageSize,
                filterParams.Order,
                items);

            return pagedList;
        }

        public async Task<AffairModel> GetByIdAsync(long id)
        {
            var affairEntity = await UnitOfWork.AffairRepository.GetAsync(item => item.Id == id);
            if (affairEntity is null) throw new Exception("Affair not found!");

            var affair = Mapper.Map<AffairModel>(affairEntity);
            return affair;
        }

        public async Task<bool> UpdateAsync(long id, AffairUpdateModel affair)
        {
            var affairEntity = await UnitOfWork.AffairRepository.GetAsync(item => item.Id == id);
            if (affairEntity is null) throw new Exception("Affair not found!");

            Mapper.Map(affair, affairEntity);
            UnitOfWork.AffairRepository.Update(affairEntity);
            await UnitOfWork.CommitAsync();
            return true;
        }
    }
}
