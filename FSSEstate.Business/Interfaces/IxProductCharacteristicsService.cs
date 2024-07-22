using FSSEstate.Core.Models.xProductCharacteristicsModels;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Business.Interfaces;

public interface IxProductCharacteristicsService
{
    public Task<bool> CreateAsync(xProductCharacteristicsCreateModel characterx);
    public Task<bool> UpdateAsync(long id, xProductCharacteristicsUpdateModel characterx);
    public Task<bool> DeleteAsync(long id);
    public Task<PagedList<xProductCharacteristicsModel>> GetAllAsync(xProductCharacteristicsFilterModel filterParams);
    public Task<xProductCharacteristicsModel> GetByIdAsync(long id);

}
