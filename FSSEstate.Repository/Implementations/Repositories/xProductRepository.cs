using FSSEstate.Repository.Interfaces.Repositories;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Context;

namespace FSSEstate.Repository.Implementations.Repositories;

public class xProductRepository : GenericRepository<xProduct>, IxProductRepository
{
    public xProductRepository(AppDbContext dbContext) : base(dbContext)
    {
        
    }
}
