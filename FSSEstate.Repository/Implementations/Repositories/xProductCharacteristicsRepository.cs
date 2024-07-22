using FSSEstate.Repository.Context;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces.Repositories;

namespace FSSEstate.Repository.Implementations.Repositories;

public class xProductCharacteristicsRepository : GenericRepository<xProductCharacteristics>, IxProductCharacteristicsRepository
{
    public xProductCharacteristicsRepository(AppDbContext dbContext) : base(dbContext)
    {
        
    }
}
