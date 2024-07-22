using FSSEstate.Repository.Context;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces.Repositories;

namespace FSSEstate.Repository.Implementations.Repositories;

public class xProductImageRepository : GenericRepository<xProductImage>, IxProductImageRepository
{
    public xProductImageRepository(AppDbContext dbContext) : base(dbContext)
    {
        
    }
}
