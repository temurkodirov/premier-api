using FSSEstate.Repository.Context;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces.Repositories;

namespace FSSEstate.Repository.Implementations.Repositories
{
    public class AgentRepository : GenericRepository<AgentEntity>, IAgentRepository
    {
        public AgentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
