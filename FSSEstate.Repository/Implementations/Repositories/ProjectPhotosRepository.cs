using FSSEstate.Repository.Context;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces.Repositories;

namespace FSSEstate.Repository.Implementations.Repositories
{
    public class ProjectPhotosRepository : GenericRepository<ProjectPhotosEntity>, IProjectPhotosRepository
    {
        public ProjectPhotosRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
