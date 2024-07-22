using FSSEstate.Repository.Context;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces.Repositories;

namespace FSSEstate.Repository.Implementations.Repositories
{
    public class AffairRepository : GenericRepository<AffairEntity>, IAffairRepository
    {
        public AffairRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
