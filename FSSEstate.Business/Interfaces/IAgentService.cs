using FSSEstate.Core.Models.Agents;
using FSSEstate.Core.Utility.Listing;

namespace FSSEstate.Business.Interfaces
{
    public interface IAgentService
    {
        public Task<bool> CreateAsync(AgentCreateModel agent);
        public Task<PagedList<AgentModel>> GetAllAsync(AgentFilterParams filterParams);
        public Task<PagedList<AgentAffairModel>> GetAllWithAffair(AgentAffairFilterParams filterParams);
        public Task<bool> UpdateAsync(AgentUpdateModel agent);
        public Task<bool> DeleteAsync(long id);
        public Task<AgentModel> GetByIdAsync(long id);
        public Task<AgentModel> GetByAccountIdAsync(long accountId);
    }
}
