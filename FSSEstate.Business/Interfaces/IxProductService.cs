using FSSEstate.Core.Models.xProductModels;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Business.Interfaces;

public interface IxProductService
{
    public Task<bool> CreateAsync(xProductCreateModel product);
    public Task<PagedList<xProductModel>> GetAllAsync(xProductFilterParams filterParams);
    public Task<bool> UpdateAsync(long id, xProductUpdateModel product);
    public Task<bool> DeleteAsync(long id);
    public Task<xProductModel> GetByIdAsync(long id);
}
