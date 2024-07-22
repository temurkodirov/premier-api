using FSSEstate.Repository.Context;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces.Repositories;

namespace FSSEstate.Repository.Implementations.Repositories
{
    public class InformationRepository : GenericRepository<InformationEntity>, IInformationRepository
    {
        public InformationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
