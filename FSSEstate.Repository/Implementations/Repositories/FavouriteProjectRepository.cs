using FSSEstate.Repository.Context;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces.Repositories;

namespace FSSEstate.Repository.Implementations.Repositories
{
    public class FavouriteProjectRepository : GenericRepository<FavouriteProjectEntity>, IFavouriteProjectRepository
    {
        public FavouriteProjectRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
