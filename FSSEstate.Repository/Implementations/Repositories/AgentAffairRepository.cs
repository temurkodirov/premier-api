using FSSEstate.Repository.Context;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces.Repositories;

namespace FSSEstate.Repository.Implementations.Repositories
{
    public class AgentAffairRepository : GenericRepository<AgentAffairEntity>, IAgentAffairRepository
    {
        public AgentAffairRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
