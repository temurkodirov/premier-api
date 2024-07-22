using FSSEstate.Repository.Context;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces.Repositories;

namespace FSSEstate.Repository.Implementations.Repositories
{
    public class InformationPhotosRepository : GenericRepository<InformationPhotosEntity>, IInformationPhotosRepository
    {
        public InformationPhotosRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
